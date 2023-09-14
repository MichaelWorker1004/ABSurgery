import { IAppUserReadOnlyModel } from '../../api';

export interface IAuthCredentials {
  userName: string;
  password: string;
}

export interface IRefreshToken {
  refreshToken: string;
}

export interface IError {
  type: string | null;
  title: string | null;
  status: number | null;
  traceId: string | null;
  errors: object | null;
}

export interface AuthStateModel {
  [key: string]: any;
  access_token: string | null;
  refresh_token: string | null;
  token_type: string | null;
  userName: string | null;
  expiration: string | null;
  expires_in_minutes: number | null;
  user: IAppUserReadOnlyModel | null;
}

export interface IAuthState extends AuthStateModel {
  claims: string[] | null;
  errors: IError | null;
  isBusy: boolean;
  isPasswordReset: boolean;
  passwordResetComplete: boolean;
  isAuthenticated: boolean;
}
