// export interface AuthStateModel {
//   token: string | null;
//   username: string | null;
// }

import { IUserCredentialModel } from '../../api/models/users/user-credential.model';

export class Login {
  static readonly type = '[Auth] Login';
  constructor(public payload: IUserCredentialModel) {}
}

export class Logout {
  static readonly type = '[Auth] Logout';
}
