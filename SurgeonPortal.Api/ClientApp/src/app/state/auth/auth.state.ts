import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { Action, State, StateContext } from '@ngxs/store';
import { Login, Logout, UpdatePassword, UpdateUsername } from './auth.actions';
import {
  AuthStateModel,
  AuthService,
  IError,
  IAuthCredentials,
} from '../../api/services/auth/auth.service';

@State<AuthStateModel>({
  name: 'auth',
  defaults: {
    access_token: null,
    refresh_token: null,
    token_type: null,
    user_name: null,
    expiration: null,
    expires_in_minutes: null,
    user: null,
    userCredentials: null,
    errors: null,
  },
})
@Injectable()
export class AuthState {
  constructor(private authService: AuthService) {}

  @Action(Login)
  login(ctx: StateContext<AuthStateModel>, action: Login) {
    return this.authService.login(action.payload).pipe(
      tap((result: AuthStateModel | IError) => {
        // eslint-disable-next-line no-prototype-builtins
        if (result.hasOwnProperty('status')) {
          ctx.setState({
            access_token: null,
            refresh_token: null,
            token_type: null,
            user_name: null,
            expiration: null,
            expires_in_minutes: null,
            user: null,
            userCredentials: null,
            errors: result,
          });
          // Clear out the errors to make alerts go away
          setTimeout(() => {
            ctx.setState({
              access_token: null,
              refresh_token: null,
              token_type: null,
              user_name: null,
              expiration: null,
              expires_in_minutes: null,
              user: null,
              userCredentials: null,
              errors: null,
            });
          }, 10000);
        } else {
          ctx.patchState(result);
        }
      })
    );
  }

  @Action(Logout)
  logout(ctx: StateContext<AuthStateModel>) {
    ctx.setState({
      access_token: null,
      refresh_token: null,
      token_type: null,
      user_name: null,
      expiration: null,
      expires_in_minutes: null,
      user: null,
      userCredentials: null,
      errors: null,
    });
  }

  @Action(UpdateUsername)
  updateUsername(ctx: StateContext<AuthStateModel>, action: UpdateUsername) {
    const state = ctx.getState();
    const userCredentials = {
      ...state.userCredentials,
      username: action.payload,
    } as IAuthCredentials;
    ctx.patchState({
      userCredentials,
    });
  }

  @Action(UpdatePassword)
  updatePassword(ctx: StateContext<AuthStateModel>, action: UpdatePassword) {
    const state = ctx.getState();
    const userCredentials = {
      ...state.userCredentials,
      password: action.payload,
    } as IAuthCredentials;
    ctx.patchState({
      userCredentials,
    });
  }
}
