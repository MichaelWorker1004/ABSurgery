import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, of } from 'rxjs';
import { IUserCredentialModel } from '../models/users/user-credential.model';

export interface IUser {
  userId: number;
  fullName: string;
  title: string;
  emailAddress: string;
}

export interface AuthStateModel {
  access_token: string | null;
  refresh_token: string | null;
  token_type: string | null;
  user_name: string | null;
  expiration: string | null;
  expires_in_minutes: number | null;
  user: IUser | null;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private httpClient: HttpClient) {}

  login(payload: IUserCredentialModel): Observable<AuthStateModel> {
    // test: test1@something.com
    // test: pass@word123
    const url = `/v1/users/authenticate?emailAddress=${payload.emailAddress}&password=${payload.password}`;
    return this.httpClient.post<AuthStateModel>(url, {}).pipe(
      map((resp) => {
        return resp;
      })
    );
  }

  logout(token: string | null): Observable<string> {
    console.log(token);
    return of('logged out');
  }
}
