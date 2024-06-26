import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import { IUserProfileModel } from '../../api';
import { IFormErrors } from '../../shared/common';
import { UserProfilesService } from '../../api';
import {
  ClearUserProfileErrors,
  GetUserProfile,
  UpdateUserProfile,
} from './user-profile.actions';
import { GetPicklists, GetStateList } from '../picklists';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

export interface IUserProfile {
  userProfileId: number;
  userId: number;
  firstName: string;
  middleName: string;
  lastName: string;
  suffix: string;
  displayName: string;
  officePhoneNumber: string;
  mobilePhoneNumber: string;
  birthCity: string;
  birthState: string;
  birthCountry: string;
  countryCitizenship: string;
  absId: string;
  certificationStatus: string;
  npi: string;
  genderId: string; // Need to cast as number on saves
  birthDate: string;
  race: string;
  ethnicity: string;
  firstLanguageId: string; // Need to cast as number on saves
  bestLanguageId: string; // Need to cast as number on saves
  receiveComms: boolean;
  userConfirmed: boolean;
  userConfirmedDate: string;
  createdByUserId: number;
  createdAtUtc: string;
  lastUpdatedAtUtc: string;
  lastLoginDateUtc: string;
  lastUpdatedByUserId: number;
  street1: string;
  street2: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
  emailAddress: string;
  fullName: string;
  profilePicture: string;
  claims: string[];
  errors?: IFormErrors | null;
}

export const USER_PROFILE_STATE_TOKEN = new StateToken<IUserProfile>(
  'userProfile'
);

@State<IUserProfile>({
  name: USER_PROFILE_STATE_TOKEN,
  defaults: {
    userProfileId: 0,
    userId: 0,
    firstName: '',
    middleName: '',
    lastName: '',
    suffix: '',
    displayName: '',
    officePhoneNumber: '',
    mobilePhoneNumber: '',
    birthCity: '',
    birthState: '',
    birthCountry: '',
    countryCitizenship: '',
    absId: '',
    certificationStatus: '',
    npi: '',
    genderId: '',
    birthDate: '',
    race: '',
    ethnicity: '',
    firstLanguageId: '',
    bestLanguageId: '',
    receiveComms: false,
    userConfirmed: false,
    userConfirmedDate: '',
    createdByUserId: 0,
    createdAtUtc: '',
    lastUpdatedAtUtc: '',
    lastLoginDateUtc: '',
    lastUpdatedByUserId: 0,
    street1: '',
    street2: '',
    city: '',
    state: '',
    zipCode: '',
    country: '',
    emailAddress: '',
    fullName: '',
    profilePicture: '',
    claims: [],
    errors: null,
  },
})
@Injectable()
export class UserProfileState {
  constructor(
    private store: Store,
    private userProfilesService: UserProfilesService,
    private globalDialogService: GlobalDialogService
  ) {}

  @Action(GetUserProfile)
  getUserProfile(
    ctx: StateContext<IUserProfile>,
    { loginUser, claims }: GetUserProfile
  ) {
    this.globalDialogService.showLoading();
    const state = ctx.getState();
    const userId = loginUser?.userId;
    return this.userProfilesService
      .retrieveUserProfile_GetByUserId(userId)
      .pipe(
        tap((result: IUserProfileModel) => {
          const res = result as unknown as IUserProfile;
          res.firstLanguageId = res.firstLanguageId?.toString();
          res.bestLanguageId = res.bestLanguageId?.toString();
          res.genderId = res.genderId?.toString();
          ctx.setState({
            ...state,
            ...loginUser,
            ...res,
            userConfirmed: false,
            claims,
            profilePicture:
              'https://fastly.picsum.photos/id/91/3504/2336.jpg?hmac=tK6z7RReLgUlCuf4flDKeg57o6CUAbgklgLsGL0UowU',
            errors: null,
          });
          this.store.dispatch(new GetStateList(ctx.getState().birthCountry));
          this.store.dispatch(new GetPicklists(ctx.getState().country));
          this.globalDialogService.closeOpenDialog();
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          return of(errors);
        })
      );
  }

  @Action(UpdateUserProfile)
  updateUserProfile(
    ctx: StateContext<IUserProfile>,
    { payload }: UpdateUserProfile
  ) {
    this.globalDialogService.showLoading();
    const model = {} as unknown as IUserProfileModel;

    ctx.setState({
      ...ctx.getState(),
      ...payload,
    });

    Object.assign(model, ctx.getState());
    const userProfile: IUserProfileModel =
      model as unknown as IUserProfileModel;
    userProfile.userConfirmedDate = new Date().toISOString();
    userProfile.userConfirmed = true;
    userProfile.genderId = +userProfile.genderId;
    userProfile.firstLanguageId = +userProfile.firstLanguageId;
    userProfile.bestLanguageId = +userProfile.bestLanguageId;
    userProfile.lastUpdatedByUserId = ctx.getState().userId;

    this.globalDialogService.showLoading();

    return this.userProfilesService
      .updateUserProfile(ctx.getState().userId, userProfile)
      .pipe(
        tap((result: IUserProfileModel) => {
          const userProfile = result as unknown as IUserProfile;
          userProfile.genderId = userProfile.genderId.toString();
          userProfile.firstLanguageId = userProfile.firstLanguageId.toString();
          userProfile.bestLanguageId = userProfile.bestLanguageId.toString();
          ctx.patchState({
            ...userProfile,
            errors: null,
          });
          this.globalDialogService.showSuccessError(
            'Success',
            'Saved successfully',
            true
          );
        }),
        catchError((error) => {
          // const errors = httpError.error;
          ctx.setState({
            ...ctx.getState(),
            errors: error.error.errors,
          });
          this.globalDialogService.closeOpenDialog();
          return of(error);
        })
      );
  }

  @Action(ClearUserProfileErrors)
  clearUserProfileErrors(ctx: StateContext<IUserProfile>) {
    ctx.patchState({
      ...ctx.getState(),
      errors: null,
    });
  }
}
