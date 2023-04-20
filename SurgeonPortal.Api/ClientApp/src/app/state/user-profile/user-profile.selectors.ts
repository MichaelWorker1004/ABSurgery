import { Selector, createPropertySelectors } from '@ngxs/store';
import { IUserProfile, UserProfileState } from './user-profile.state';

export class UserProfileSelectors {
  static slices = createPropertySelectors<IUserProfile>(UserProfileState);

  @Selector([UserProfileState])
  static user(state: IUserProfile): IUserProfile | undefined {
    if (state?.userId > -1) {
      return state;
    }
    return;
  }

  @Selector([UserProfileState])
  static isUserProfileLoaded(state: IUserProfile): boolean {
    return state?.userId > -1;
  }

  @Selector([UserProfileState])
  static userDisplayName(state: IUserProfile): string | undefined {
    if (state?.displayName.length > 0) {
      return state.displayName;
    }
    return;
  }

  @Selector([UserProfileState])
  static userClaims(state: IUserProfile): string[] | undefined {
    if (state?.claims.length > 0) {
      return state.claims;
    }
    return;
  }

  @Selector([UserProfileState])
  static profilePicture(state: IUserProfile): string | undefined {
    console.log('profilePicture', state?.profilePicture);
    if (state?.profilePicture.length > 0) {
      return state.profilePicture;
    }
    return;
  }
}
