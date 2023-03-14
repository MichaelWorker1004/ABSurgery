import { Injectable } from '@angular/core';
import { tap, map, flatMap } from 'rxjs/operators';
import { HttpHeaders } from '@angular/common/http';

import { ApiService } from '../api.service';
import { AccountService } from '../account';

import { ResponseObservable } from '../api.model';
import { ConnectToken } from './connect.model';

@Injectable()
export class ConnectService {
  private readonly loginHeaders = new HttpHeaders({
    'Content-Type': 'application/x-www-form-urlencoded',
  });

  constructor(
    private apiService: ApiService,
    private accountService: AccountService
  ) {}

  /**
   * Logs the user in and sets the token automatically for all subsequent calls through the api services.
   * @param email The user email
   * @param password The user password
   */
  public login(
    email: string,
    password: string,
    remember: boolean,
    anonymous?: boolean,
    projectId?: number
  ): ResponseObservable<ConnectToken> {
    // the login call doesn"t accept json so we use custom headers
    return this.apiService
      .postForm<ConnectToken>(
        'connect/token',
        {
          username: email,
          password: password,
          grant_type: 'password',
        },
        {
          anonymous: anonymous,
          projectId: projectId,
        },
        this.loginHeaders
      )
      .pipe(
        tap((response) => {
          // set our token for api calls
          this.apiService.setToken(response.access_token, remember);
        }),
        flatMap((response) => {
          // get our initial account information
          return this.accountService.get().pipe(map(() => response));
        })
      );
  }

  /**
   * Logs the user out.
   */
  public logout() {
    this.apiService.clearToken();
  }
}
