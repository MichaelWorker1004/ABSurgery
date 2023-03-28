import { Injectable } from '@angular/core';
import {
  Action,
  Selector,
  State,
  StateContext,
  StateToken,
  Store,
} from '@ngxs/store';
import { SaveMyAccountChanges } from './my-account.actions';
import { IUserCredentialModel } from '../../api/models/users/user-credential.model';
import { UserCredentialsService } from '../../api/services/users/user-credentials.service';
import { of, tap } from 'rxjs';
import { Logout } from '../auth/auth.actions';
import { catchError } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';

export interface IUserCredential extends IUserCredentialModel {
  errors?: IFormErrors | null;
}
export interface IFormErrors {
  [key: string]: string[];
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
  @Selector()
  static user(state: IUserCredential) {
    return state;
  }

  @Selector()
  static errors(state: IUserCredential): IFormErrors | null {
    return <IFormErrors>state.errors;
  }
  constructor(
    private authStore: Store,
    private UserCredentialsService: UserCredentialsService
  ) {}

  @Action(SaveMyAccountChanges)
  saveMyAccountChanges(
    ctx: StateContext<IUserCredential>,
    { payload }: SaveMyAccountChanges
  ) {
    return this.UserCredentialsService.updateUserCredential(payload).pipe(
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
