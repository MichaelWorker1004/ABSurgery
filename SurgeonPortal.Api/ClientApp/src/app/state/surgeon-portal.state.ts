import { AuthState } from './auth';
import { DashboardState } from './dashboard';
import { MyAccountState } from './my-account';
import { UserProfileState } from './user-profile';
import { PicklistsState } from './picklists/picklists.state';
import { ContinuousCertificationState } from './continuous-certification';

export const surgeonPortalState = [
  AuthState,
  MyAccountState,
  UserProfileState,
  PicklistsState,
  DashboardState,
  ContinuousCertificationState,
];
