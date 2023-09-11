import { AuthService } from '../../../api';
import {
  HttpErrorResponse,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Store } from '@ngxs/store';
import { Logout } from '../../../state';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private auth: AuthService, private store: Store) {}

  getAuthorizationToken(): string | undefined {
    const accessToken = sessionStorage.getItem('access_token');
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
        })
      );
    }
    const authReq = req.clone({
      headers: req.headers.set('Authorization', authToken),
    });

    // send cloned request with header to the next handler.
    return next.handle(authReq).pipe(
      catchError((err) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401) {
            this.store.dispatch(new Logout());
          }
        }
        throw err;
      })
    );
  }
}
