import { IAuthCredentials } from '../../api';
export class Login {
  static readonly type = '[Auth] Login';
  constructor(public payload: any) {}
}

export class Logout {
  static readonly type = '[Auth] Logout';
}

export class ClearAuthErrors {
  static readonly type = '[Auth] Clear Erros';
}
