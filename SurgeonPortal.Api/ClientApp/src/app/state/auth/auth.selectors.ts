import { Selector } from '@ngxs/store';
import {
  AuthStateModel,
  IError,
  IUser,
} from '../../api/services/auth/auth.service';
import { AuthState } from './auth.state';

export class AuthSelectors {
  @Selector([AuthState])
  static token(state: AuthStateModel): string | null {
    return state.access_token;
  }

  @Selector([AuthState])
  static getErrors(state: AuthStateModel): IError | null {
    return state.errors as IError;
  }

  @Selector([AuthState])
  static isAuthenticated(state: AuthStateModel): boolean {
    return !!state.access_token;
  }

  @Selector([AuthState])
  static getUser(state: AuthStateModel): IUser | null {
    if (state.user) {
      return state.user;
    } else {
      return null;
    }
  }

  static parseJwt(token: string): object {
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

    return JSON.parse(jsonPayload);
  }
}
