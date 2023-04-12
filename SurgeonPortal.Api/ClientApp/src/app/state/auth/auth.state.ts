import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import { Login, Logout } from './auth.actions';
import {
  AuthStateModel,
  AuthService,
  IError,
  IAppUserReadOnlyModel,
} from '../../api';

export interface IAuthState extends AuthStateModel {
  claims: string[] | null;
  errors: IError | null;
}

export const AUTH_STATE_TOKEN = new StateToken<IAuthState>('auth');

@State<IAuthState>({
  name: AUTH_STATE_TOKEN,
  defaults: {
    access_token: null,
    refresh_token: null,
    token_type: null,
    user_name: null,
    expiration: null,
    expires_in_minutes: null,
    user: null,
    claims: null,
    errors: null,
  },
})
@Injectable()
export class AuthState {
  constructor(private authService: AuthService, private store: Store) {}

  @Action(Login)
  login(ctx: StateContext<IAuthState>, action: Login) {
    return this.authService.login(action.payload).pipe(
      tap((result: AuthStateModel | IError) => {
        // eslint-disable-next-line no-prototype-builtins
        if (result.hasOwnProperty('status')) {
          ctx.setState({
            access_token: '',
            refresh_token: '',
            token_type: '',
            user_name: '',
            expiration: '',
            expires_in_minutes: 0,
            user: {} as IAppUserReadOnlyModel,
            claims: [],
            errors: <IError>result,
          });
        } else {
          const state = ctx.getState();
          const res = result as AuthStateModel;
          ctx.setState({
            ...state,
            ...result,
            claims: AuthState.parseJwt(<string>res.access_token).claims,
            errors: null,
          });
        }
      })
    );
  }

  @Action(Logout)
  logout(ctx: StateContext<IAuthState>) {
    sessionStorage.clear();
    this.store.reset({});
    ctx.setState({
      access_token: '',
      refresh_token: '',
      token_type: '',
      user_name: '',
      expiration: '',
      expires_in_minutes: 0,
      user: {} as IAppUserReadOnlyModel,
      claims: [],
      errors: null,
    });
  }

  static parseJwt(token: string): { claims: string[] } {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      window
        .atob(base64)
        .split('')
        .map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join('')
    );

    const returnObj = JSON.parse(jsonPayload);
    returnObj.claims =
      returnObj['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

    return returnObj;
  }
}
