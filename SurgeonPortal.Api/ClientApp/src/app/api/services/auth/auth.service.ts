// TODO - Convert this to use the setup for YTGIM, currently this is hand codede
import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { map, Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IAuthCredentials, IAuthState, IError, IRefreshToken } from "../../../state";
import { ApiService } from "ytg-angular";


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  token: string | undefined;
  constructor(private apiService: ApiService) {}

  login(payload: IAuthCredentials): Observable<IAuthState | IError> {
    return this.apiService
      .post<IAuthState>(`/api/users/authenticate`, {
        userName: payload.userName,
        password: payload.password,
      })
      .pipe(
        map((resp) => {
          sessionStorage.setItem('access_token', <string>resp.access_token);
          return resp;
        }),
        catchError((err: HttpErrorResponse) => {
          return of(err.error as IError);
        })
      );
  }

  refreshToken(payload: IRefreshToken): Observable<IAuthState | IError> {
    return this.apiService
      .post<IAuthState>(`/api/users/refresh`, {
        refreshToken: payload.refreshToken,
      })
      .pipe(
        map((resp) => {
          sessionStorage.setItem('access_token', <string>resp.access_token);
          return resp;
        }),
        catchError((err: HttpErrorResponse) => {
          return of(err.error as IError);
        })
      );
  }

  resetPassword(body: {oldPassword: string, newPassword: string}): Observable<boolean | IError> {
    return this.apiService.post<boolean>(`/api/users/reset-password`, {
      oldPassword: body.oldPassword,
      newPassword: body.newPassword,
    }).pipe(
      map((result) => {
        return true;
      }),
      catchError((err: HttpErrorResponse) => {
        return of(err.error as IError);
      })
    );
  }

}
