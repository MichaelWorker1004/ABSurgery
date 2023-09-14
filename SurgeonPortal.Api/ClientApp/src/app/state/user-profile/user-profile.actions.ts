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

export class LinkUserData {
  static readonly type = '[UserProfile] Get the user profile values';
}

export class ClearUserProfileErrors {
  static readonly type = '[UserProfile] Clear the user profile errors';
}
