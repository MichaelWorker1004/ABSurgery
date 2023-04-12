import { Injectable } from '@angular/core';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import { of, tap } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { SaveMyAccountChanges } from './my-account.actions';
import { IFormErrors } from '../../shared/common';
import { IUserCredentialModel } from '../../api/models/users/user-credential.model';
import { UserCredentialsService } from '../../api/services/users/user-credentials.service';
import { Logout } from '../auth';

export interface IUserCredential extends IUserCredentialModel {
  errors?: IFormErrors | null;
}

const USER_ACCOUNT_STATE_TOKEN = new StateToken<IUserCredential>('userAccount');
@State<IUserCredential>({
  name: USER_ACCOUNT_STATE_TOKEN,
  defaults: {
    emailAddress: null,
    password: null,
    errors: null,
  },
})
@Injectable()
export class MyAccountState {
  constructor(
    private authStore: Store,
    private userCredentialsService: UserCredentialsService
  ) {}

  @Action(SaveMyAccountChanges)
  saveMyAccountChanges(
    ctx: StateContext<IUserCredential>,
    { payload }: SaveMyAccountChanges
  ) {
    return this.userCredentialsService.updateUserCredential(payload).pipe(
      tap((result: IUserCredentialModel) => {
        // Succeeded in changing the user's credentials so logout
        ctx.setState({
          emailAddress: null,
          password: null,
          errors: null,
        });
        this.authStore.dispatch(new Logout());
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        return of(errors);
      })
    );
  }
}
