import { AuthService } from '../../api/services/auth/auth.service';
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
import { Logout } from '../../state/auth/auth.actions';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private auth: AuthService, private store: Store) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    // Get the auth token from the service.
    const authToken = this.auth.getAuthorizationToken();

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
