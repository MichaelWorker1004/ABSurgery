import { IAuthCredentials } from '../../api';
export class Login {
  static readonly type = '[Auth] Login';
  constructor(public payload: IAuthCredentials) {}
}

export class Logout {
  static readonly type = '[Auth] Logout';
}
