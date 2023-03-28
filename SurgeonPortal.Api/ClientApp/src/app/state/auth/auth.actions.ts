import { IAuthCredentials } from '../../api/services/auth/auth.service';
export class Login {
  static readonly type = '[Auth] Login';
  constructor(public payload: IAuthCredentials) {}
}

export class Logout {
  static readonly type = '[Auth] Logout';
}

export class UpdateUsername {
  static readonly type = '[Auth] UpdateUsername';
  constructor(public payload: string) {}
}

export class UpdatePassword {
  static readonly type = '[Auth] UpdatePassword';
  constructor(public payload: string) {}
}
