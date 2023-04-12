import { IUserProfile } from './user-profile.state';
import { IAppUserReadOnlyModel } from '../../api';

export class GetUserProfile {
  static readonly type = '[UserProfile] Get the user profile';

  constructor(
    public loginUser: IAppUserReadOnlyModel,
    public claims: string[]
  ) {}
}

export class UpdateUserProfile {
  static readonly type = '[UserProfile] Update the user profile';

  constructor(public payload: IUserProfile) {}
}
