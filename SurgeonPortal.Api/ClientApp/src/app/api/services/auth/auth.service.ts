// TODO - Convert this to use the setup for YTGIM, currently this is hand codede
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { map, Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IAppUserReadOnlyModel } from "../../models/users/app-user-read-only.model";
import { IAuthState } from "../../../state";

export interface IAuthCredentials {
  emailAddress: string;
  password: string;
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
  user_name: string | null;
  expiration: string | null;
  expires_in_minutes: number | null;
  user: IAppUserReadOnlyModel | null;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  token: string | undefined;
  constructor(private httpClient: HttpClient) {}

  login(payload: IAuthCredentials): Observable<IAuthState | IError> {
    return this.httpClient
      .post<IAuthState>(`/api/users/authenticate`, {
        emailAddress: payload.emailAddress,
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
}
