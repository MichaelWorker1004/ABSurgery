import { Selector, createPropertySelectors } from '@ngxs/store';
import { IUserProfile, UserProfileState } from './user-profile.state';

export class UserProfileSelectors {
  static slices = createPropertySelectors<IUserProfile>(UserProfileState);

  @Selector([UserProfileState])
  static user(state: IUserProfile): IUserProfile | undefined {
    if (state?.userId) {
      return state;
    }
    return;
  }

  @Selector([UserProfileState])
  static isUserProfileLoaded(state: IUserProfile): boolean {
    return !!state?.userId;
  }

  @Selector([UserProfileState])
  static userDisplayName(state: IUserProfile): string | undefined {
    if (state?.displayName?.length) {
      return state.displayName;
    }
    return;
  }

  @Selector([UserProfileState])
  static userId(state: IUserProfile): number | undefined {
    if (state?.userId) {
      return state.userId;
    }
    return;
  }

  @Selector([UserProfileState])
  static userClaims(state: IUserProfile): string[] | undefined {
    if (state?.claims.length) {
      return state.claims;
    }
    return;
  }

  @Selector([UserProfileState])
  static profilePicture(state: IUserProfile): string | undefined {
    if (state?.profilePicture?.length) {
      return state.profilePicture;
    }
    return;
  }
}
