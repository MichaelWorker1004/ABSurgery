import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { map, Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

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

export interface IUser {
  userId: number | null;
  fullName: string | null;
  title: string | null;
  emailAddress: string | null;
  mailingList?: boolean | null;
  status?: string | null;
}

export interface AuthStateModel {
  access_token: string | null;
  refresh_token: string | null;
  token_type: string | null;
  user_name: string | null;
  expiration: string | null;
  expires_in_minutes: number | null;
  user: IUser | null;
  userCredentials?: IAuthCredentials | null;
  errors?: object | null;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private httpClient: HttpClient) {}

  getAuthorizationToken(): string | undefined {
    const authString = sessionStorage.getItem('auth');
    const auth: AuthStateModel = authString ? JSON.parse(authString) : null;
    if (auth) {
      return `Bearer ${auth?.access_token}`;
    }
    sessionStorage.clear();
    return;
  }

  login(payload: IAuthCredentials): Observable<AuthStateModel | IError> {
    return this.httpClient
      .post<AuthStateModel>(`/v1/users/authenticate`, {
        emailAddress: payload.emailAddress,
        password: payload.password,
      })
      .pipe(
        map((resp) => {
          return resp;
        }),
        catchError((err: HttpErrorResponse) => {
          return of(err.error as IError);
        })
      );
  }
}
