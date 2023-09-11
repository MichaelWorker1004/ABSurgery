import { createPropertySelectors, Selector } from '@ngxs/store';
import { IAppUserReadOnlyModel } from '../../api';
import { AuthState } from './auth.state';
import { IAuthState, IError } from './auth.interfaces';

export class AuthSelectors {
  static slices = createPropertySelectors<IAuthState>(AuthState);
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
  static getUserId(state: IAuthState): number | undefined {
    return state.user?.userId;
  }
}
