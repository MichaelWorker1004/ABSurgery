import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { Login, Logout } from './auth/ngxs-auth.model';
import { AuthStateModel, AuthService } from '../api/services/auth.service';

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
  },
})
@Injectable()
export class AuthState {
  @Selector()
  static token(state: AuthStateModel): string | null {
    return state.access_token;
  }

  @Selector()
  static isAuthenticated(state: AuthStateModel): boolean {
    return !!state.access_token;
  }

  // static getAuthenticated(state: AuthState): boolean {
  //   return;
  // }

  constructor(private authService: AuthService) {}

  @Action(Login)
  login(ctx: StateContext<AuthStateModel>, action: Login) {
    return this.authService.login(action.payload).pipe(
      tap((result: AuthStateModel) => {
        ctx.patchState(result);
      })
    );
  }

  @Action(Logout)
  logout(ctx: StateContext<AuthStateModel>) {
    const state = ctx.getState();
    return this.authService.logout(state.access_token).pipe(
      tap(() => {
        ctx.setState({} as AuthStateModel);
      })
    );
  }
}
