import { IUserAccountModel } from '../../api/models/user-account.model';

export class SetUser {
  static readonly type = '[MyAccount] Set the properties of My Account User';
  constructor(public payload: IUserAccountModel) {}
}

export class ResetUser {
  static readonly type = '[MyAccount] Reset the user to the original state';
}

export class GetUser {
  static readonly type = '[MyAccount] Get the user';
}

export class SetEmail {
  static readonly type = '[MyAction] Set email';
}
