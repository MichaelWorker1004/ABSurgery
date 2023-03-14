import { Injectable } from '@angular/core';

import { ApiService } from '../api.service';
import { ResponseObservable } from '../api.model';

import { Account } from './account.model';
import { stateAccount, stateAuth } from '../../state';
import { AppState, StateMode } from '../../state';
import { Authorize } from '../api.authorize';
import { StateService, StateChange } from '../../state';
import { tap } from 'rxjs/operators';

@Injectable()
export class AccountService {
  // @LocalStorage()
  private savedAccount: Account;
  @AppState(stateAccount, StateMode.ReadWrite)
  private account: Account;

  constructor(
    private apiService: ApiService,
    private stateService: StateService
  ) {
    this.account =
      this.apiService.getToken() != null ? this.savedAccount : null;
    this.savedAccount = this.account;
    this.stateService.subscribe<boolean>(stateAuth, (change) =>
      this.authChanged(change)
    );
  }

  private authChanged(change: StateChange<boolean>): any {
    if (change.previous && !change.value) {
      this.account = null;
    }
  }

  /**
   * Gets the account info for the logged in user.
   * Will set the account info app wide
   */
  @Authorize()
  public get(): ResponseObservable<Account> {
    return this.apiService.get<Account>('account').pipe(
      tap((response) => {
        if (!response) {
          return;
        }
        this.account = response;
        this.savedAccount = response;
      })
    );
  }

  /**
   * Resets the password of the account
   */
  public resetPassword(
    token: string,
    userId: number,
    password: string
  ): ResponseObservable<string> {
    return this.apiService.post<string>('account/password', {
      token: token,
      userId: userId,
      password: password,
    });
  }
}
