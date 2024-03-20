/* eslint-disable prettier/prettier */
import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import { of, tap } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { IUserCredentialModel, UserCredentialsService } from '../../api';
import { IFormErrors } from '../../shared/common';
import { ClearErrors, SaveMyAccountChanges } from './my-account.actions';

export interface IUserCredential extends IUserCredentialModel {
  errors?: IFormErrors | null;
}

const USER_ACCOUNT_STATE_TOKEN = new StateToken<IUserCredential>('userAccount');
@State<IUserCredential>({
  name: USER_ACCOUNT_STATE_TOKEN,
  defaults: {
    userId: null,
    emailAddress: null,
    password: null,
    errors: null,
  },
})
@Injectable()
export class MyAccountState {
  constructor(
    private authStore: Store,
    private userCredentialsService: UserCredentialsService,
    private globalDialogService: GlobalDialogService
  ) {}

  @Action(SaveMyAccountChanges)
  saveMyAccountChanges(
    ctx: StateContext<IUserCredential>,
    { payload }: SaveMyAccountChanges
  ) {
    this.globalDialogService.showLoading();
    return this.userCredentialsService.updateUserCredential(payload).pipe(
      tap((result: IUserCredentialModel) => {
        let message = 'Saved scuccessfully.';
        if (payload.password && payload.password.length > 0) {
          message =
            message +
            '<br>For Security purposes please login again with your updated information.';
        }
        // Succeeded in changing the user's credentials so logout
        ctx.setState({
          userId: null,
          emailAddress: null,
          password: null,
          errors: null,
        });
        this.globalDialogService.showSuccessError('Success', message, true);
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });

        this.globalDialogService.showSuccessError(
          'Error',
          JSON.stringify(errors),
          false
        );
        return of(errors);
      })
    );
  }

  @Action(ClearErrors)
  clearErrors(ctx: StateContext<IUserCredential>) {
    ctx.patchState({ errors: null });
  }
}
