import { Selector } from '@ngxs/store';
import { IAppUserReadOnlyModel, IError } from '../../api';
import { AuthState, IAuthState } from './auth.state';

export class AuthSelectors {
  @Selector([AuthState])
  static accessToken(state: IAuthState): string | undefined {
    if (state?.access_token && state?.access_token?.length > 0) {
      return state.access_token;
    }
    return undefined;
  }

  @Selector([AuthState])
  static refreshToken(state: IAuthState): string | undefined {
    if (state?.refresh_token && state?.refresh_token?.length > 0) {
      return state.refresh_token;
    }
    return undefined;
  }

  @Selector([AuthState])
  static claims(state: IAuthState): string[] | undefined {
    if (state?.claims && state?.claims?.length > 0) {
      return state.claims;
    }
    return undefined;
  }

  @Selector([AuthState])
  static loginUser(state: IAuthState): IAppUserReadOnlyModel | undefined {
    return state.user as IAppUserReadOnlyModel;
  }

  @Selector([AuthState])
  static getErrors(state: IAuthState): IError | null {
    return state.errors as IError;
  }

  @Selector([AuthState])
  static isAuthenticated(state: IAuthState): boolean {
    return !!state.access_token;
  }

  @Selector([AuthState])
  static getUserId(state: IAuthState): number | undefined {
    return state.user?.userId;
  }
}
