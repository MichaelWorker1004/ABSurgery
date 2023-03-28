import { IUserCredential } from './my-account.state';

export class SaveMyAccountChanges {
  static readonly type = '[MyAccount] Save My Account Changes';
  constructor(public payload: IUserCredential) {}
}
