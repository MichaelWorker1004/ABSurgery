import {
  HttpErrorResponse,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';

import { GlobalToastService } from '../../services/global-toast.service';

import { Store } from '@ngxs/store';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private store: Store,
    private globalToastService: GlobalToastService
  ) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const errorReq = req.clone();

    // send cloned request with header to the next handler.
    return next.handle(errorReq).pipe(
      catchError((err) => {
        if (err instanceof HttpErrorResponse) {
          // do not handle 401/404 errors here, they will be handled in the auth interceptor or the app respectively
          // carve out specifically for the Login Failed error as it is shaped differently from other errors
          if (
            (!err.error ||
              (typeof err.error === 'string' &&
                err.error !== 'Login failed')) &&
            err.status !== 401 &&
            err.status !== 403 &&
            err.status !== 404
          ) {
            // TODO: [Joe] - add error specific messages here

            const message = `${err.status} Error: ${err.statusText}`;
            // if (err.error) {
            //   message = err.error;
            // }
            this.globalToastService.showError(message);
          }
          //throw err;
          // log error to console for debugging
          console.log(err);
          console.log(`Error Code: ${err.status}\nMessage: ${err.message}`);
        }

        // always allow the error to continue to propagate
        throw err;
      })
    );
  }
}
