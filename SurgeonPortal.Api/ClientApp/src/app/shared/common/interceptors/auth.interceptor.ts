import { AuthService } from '../../../api';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { catchError, filter, switchMap, take } from 'rxjs/operators';
import { Store } from '@ngxs/store';
import { Logout, RefreshToken } from '../../../state';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(
    null
  );
  constructor(private auth: AuthService, private store: Store) {}

  getAuthorizationToken(): string | undefined {
    //const accessToken = sessionStorage.getItem('access_token');
    const accessToken = this.store.selectSnapshot(
      (state) => state.auth.access_token
    );
    if (accessToken) {
      return `Bearer ${accessToken}`;
    }
    sessionStorage.clear();
    return;
  }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    // Get the auth token from the service.
    const authToken = this.getAuthorizationToken();

    // Clone the request and replace the original headers with
    // cloned headers, updated with the authorization.
    if (!authToken) {
      return next.handle(req).pipe(
        tap(() => {
          console.info('AuthInterceptor: No auth token found');
          this.handleAuthError(req, next);
        })
      );
    }
    const authReq = this.addToken(req, authToken);

    // send cloned request with header to the next handler.
    return next.handle(authReq).pipe(
      catchError((err) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401 || err.status === 403) {
            this.handleAuthError(req, next);
          }
        }
        throw err;
      })
    );
  }

  private addToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
    return req.clone({
      headers: req.headers.set('Authorization', token),
    });
  }

  private handleAuthError(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      return this.store.dispatch(new RefreshToken()).pipe(
        switchMap((token: any) => {
          this.isRefreshing = false;
          this.refreshTokenSubject.next(token.access_token);
          return next.handle(this.addToken(req, token.access_token));
        }),
        catchError((err) => {
          this.store.dispatch(new Logout());
          throw err;
        })
      );
    } else {
      return this.refreshTokenSubject.pipe(
        filter((token) => token != null),
        take(1),
        switchMap((token) => {
          return next.handle(this.addToken(req, token));
        })
      );
    }
  }
}
