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
          // do not handle 401 errors here, they will be handled in the auth interceptor
          if (!err.error || typeof err.error === 'string') {
            if (err.status !== 401) {
              // TODO: [Joe] - add error specific messages here

              let message = `${err.status} Error: ${err.statusText}`;
              // if (err.error) {
              //   message = err.error;
              // }
              this.globalToastService.showError(message);
            }
          }
          // log error to console for debugging
          console.log(err);
          console.log(`Error Code: ${err.status}\nMessage: ${err.message}`);
          throw err;
        }
        throw err;
      })
    );
  }
}
