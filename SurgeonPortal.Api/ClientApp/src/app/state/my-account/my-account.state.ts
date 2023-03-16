import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext, StateToken } from '@ngxs/store';
import {
  IAddress,
  IUserAccountModel,
} from '../../api/models/user-account.model';
import { GetUser, SetUser, ResetUser, SetEmail } from './my-account.actions';
import { UserAccountService } from '../../api/fake/user-account.service';
import { tap } from 'rxjs';

const USER_ACCOUNT_STATE_TOKEN = new StateToken<IUserAccountModel>(
  'userAccount'
);
@State<IUserAccountModel>({
  name: USER_ACCOUNT_STATE_TOKEN,
  defaults: {
    user: {
      givenName: '',
      surName: '',
      title: '',
      locale: '',
      address: {
        street: '',
        city: '',
        state: '',
        postalCode: '',
      },
      email: '',
      confirmEmail: '',
      password: '',
      confirmPassword: '',
      mailingList: true,
      status: '',
    },
  },
})
@Injectable()
export class MyAccountState {
  @Selector()
  static user(state: IUserAccountModel) {
    return state;
  }
  constructor(private userAccountService: UserAccountService) {}
  @Action(GetUser)
  getUser({ getState, setState }: StateContext<IUserAccountModel>) {
    return this.userAccountService.getUser().pipe(
      tap((userAccount) => {
        const user = userAccount.user;
        const state = getState();
        setState({
          ...state,
          user,
        });
      })
    );
  }
  @Action(SetUser)
  addTodo(
    { getState, setState }: StateContext<IUserAccountModel>,
    { payload }: SetUser
  ) {
    return this.userAccountService.setUser(payload).pipe(
      tap((userAccount) => {
        const user = userAccount.user;
        const state = getState();
        setState({
          ...state,
          user,
        });
      })
    );
  }

  // @Action(SetEmail)
  // setEmail({});
}
