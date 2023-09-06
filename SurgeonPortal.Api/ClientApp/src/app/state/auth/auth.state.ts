import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import { Login, Logout, ClearAuthErrors, RefreshToken } from './auth.actions';
import {
  AuthStateModel,
  AuthService,
  IError,
  IAppUserReadOnlyModel,
} from '../../api';
import { Router } from '@angular/router';

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
    userName: null,
    expiration: null,
    expires_in_minutes: null,
    user: null,
    claims: null,
    errors: null,
  },
})
@Injectable()
export class AuthState {
  refreshTimer: any;
  constructor(
    private authService: AuthService,
    private store: Store,
    private router: Router
  ) {}

  @Action(Login)
  login(ctx: StateContext<IAuthState>, action: Login) {
    return this.authService.login(action.payload).pipe(
      tap((result: AuthStateModel | IError) => {
        if (typeof result === 'string' && result === 'Login failed') {
          ctx.setState({
            access_token: '',
            refresh_token: '',
            token_type: '',
            userName: '',
            expiration: '',
            expires_in_minutes: 0,
            user: {} as IAppUserReadOnlyModel,
            claims: [],
            errors: {
              type: 'Login failed',
              title: 'Login failed',
              status: 400,
              traceId: '',
              errors: null,
            },
          });
        }
        // eslint-disable-next-line no-prototype-builtins
        else if (result.hasOwnProperty('status')) {
          ctx.setState({
            access_token: '',
            refresh_token: '',
            token_type: '',
            userName: '',
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
          if (res.expires_in_minutes) {
            this.setRefreshTimer(res.expires_in_minutes);
          }
          this.router.navigate([action.payload.returnUrl ?? '/']);
        }
      })
    );
  }

  @Action(RefreshToken)
  refreshToken(
    ctx: StateContext<IAuthState>,
    payload: { refreshToken: string }
  ) {
    return this.authService.refreshToken(payload).pipe(
      tap((result: AuthStateModel | IError) => {
        // eslint-disable-next-line no-prototype-builtins
        if (!result.hasOwnProperty('status')) {
          const state = ctx.getState();
          const res = result as AuthStateModel;
          ctx.setState({
            ...state,
            ...result,
            claims: AuthState.parseJwt(<string>res.access_token).claims,
            errors: null,
          });
          if (res.expires_in_minutes) {
            this.setRefreshTimer(res.expires_in_minutes);
          }
        }
      })
    );
  }

  @Action(Logout)
  logout(ctx: StateContext<IAuthState>) {
    if (this.refreshTimer) {
      clearTimeout(this.refreshTimer);
    }
    sessionStorage.clear();
    this.store.reset({});
    ctx.setState({
      access_token: '',
      refresh_token: '',
      token_type: '',
      userName: '',
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

  @Action(ClearAuthErrors)
  clearErrors(ctx: StateContext<IAuthState>) {
    ctx.patchState({ errors: null });
  }

  private setRefreshTimer(expiresInMinutes = 0) {
    if (this.refreshTimer) {
      clearTimeout(this.refreshTimer);
    }
    let expires = expiresInMinutes - 1;
    if (expires < 0) {
      expires = 0;
    }
    this.refreshTimer = setTimeout(() => {
      this.store.dispatch(
        new RefreshToken(
          this.store.selectSnapshot((state) => state.auth.refresh_token)
        )
      );
    }, expires * 60000);
  }
}
