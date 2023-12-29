import { IForgotUsernameReadOnlyModel } from 'src/app/api/models/auth/forgot-username-read-only.model';
import { IAuthCredentials } from './auth.interfaces';
import { IForgotPasswordReadOnlyModel } from 'src/app/api/models/auth/forgot-password-read-only.model';
import { IResetForgotPasswordReadOnlyModel } from 'src/app/api/models/auth/reset-forgot-password-read-only-model';

export class Login {
  static readonly type = '[Auth] Login';
  constructor(public payload: IAuthCredentials) {}
}

export class RefreshToken {
  static readonly type = '[Auth] Refresh Token';
  constructor(public refreshToken?: string) {}
}

export class ResetPassword {
  static readonly type = '[Auth] Reset Password';
  constructor(public payload: { oldPassword: string; newPassword: string }) {}
}

export class Logout {
  static readonly type = '[Auth] Logout';
}

export class ClearAuthErrors {
  static readonly type = '[Auth] Clear Erros';
}

export class ForgotUsername {
  static readonly type = '[Auth] Forgot Username';
  constructor(public model: IForgotUsernameReadOnlyModel) {}
}

export class ForgotPassword {
  static readonly type = '[Auth] Forgot Password';
  constructor(public model: IForgotPasswordReadOnlyModel) {}
}

export class ResetForgotPassword {
  static readonly type = '[Auth] Reset Forgot Password';
  constructor(public model: IResetForgotPasswordReadOnlyModel) {}
}
