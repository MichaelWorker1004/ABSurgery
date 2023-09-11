// TODO - Convert this to use the setup for YTGIM, currently this is hand codede
import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { map, Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IAppUserReadOnlyModel } from '../../models/users/app-user-read-only.model';
import { IAuthState } from '../../../state';
import { ApiService } from "ytg-angular";

export interface IAuthCredentials {
  userName: string;
  password: string;
}

export interface IRefreshToken {
  refreshToken: string;
}

export interface IError {
  type: string | null;
  title: string | null;
  status: number | null;
  traceId: string | null;
  errors: object | null;
}

export interface AuthStateModel {
  access_token: string | null;
  refresh_token: string | null;
  token_type: string | null;
  userName: string | null;
  expiration: string | null;
  expires_in_minutes: number | null;
  user: IAppUserReadOnlyModel | null;
}

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
          //sessionStorage.setItem('access_token', <string>resp.access_token);
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
          //sessionStorage.setItem('access_token', <string>resp.access_token);
          return resp;
        }),
        catchError((err: HttpErrorResponse) => {
          return of(err.error as IError);
        })
      );
  }
}
