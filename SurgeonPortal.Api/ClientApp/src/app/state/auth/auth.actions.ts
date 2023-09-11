import { IAuthCredentials } from './auth.interfaces';

export class Login {
  static readonly type = '[Auth] Login';
  constructor(public payload: IAuthCredentials) {}
}

export class RefreshToken {
  static readonly type = '[Auth] Refresh Token';
  constructor(public refreshToken: string) {}
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
