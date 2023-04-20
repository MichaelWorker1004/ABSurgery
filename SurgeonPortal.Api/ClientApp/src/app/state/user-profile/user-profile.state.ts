import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import {
  IAppUserReadOnlyModel,
  ICountryReadOnlyModel,
  IGenderReadOnlyModel,
  ILanguageReadOnlyModel,
  IRaceReadOnlyModel,
  IStateReadOnlyModel,
  IUserProfileModel,
} from '../../api';
import { IFormErrors } from '../../shared/common';
import { UserProfilesService } from '../../api';
import {
  GetUserProfile,
  LinkUserData,
  UpdateUserProfile,
} from './user-profile.actions';
import {
  GetPicklists,
  IPicklist,
  PicklistsSelectors,
  PicklistsState,
} from '../picklists';
import { AuthSelectors } from '../auth';

export interface IUserProfile extends IUserProfileModel, IAppUserReadOnlyModel {
  countryDO?: ICountryReadOnlyModel;
  countryBirthDO?: ICountryReadOnlyModel;
  countryCitizenDO?: ICountryReadOnlyModel;
  stateDO?: IStateReadOnlyModel;
  stateBirthDO?: IStateReadOnlyModel;
  genderDO?: IGenderReadOnlyModel;
  raceDO?: IRaceReadOnlyModel;
  ethnicityDO?: IRaceReadOnlyModel;
  firstLanguageDO?: ILanguageReadOnlyModel;
  bestLanguageDO?: ILanguageReadOnlyModel;
  claims: string[];
  profilePicture: string; // TODO: <API>This is not generated
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
    nPI: '',
    genderId: 0,
    birthDate: '',
    race: '',
    ethnicity: '',
    firstLanguageId: 0,
    bestLanguageId: 0,
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
    private userProfilesService: UserProfilesService
  ) {}

  @Action(GetUserProfile)
  getUserProfile(
    ctx: StateContext<IUserProfile>,
    { loginUser, claims }: GetUserProfile
  ) {
    const state = ctx.getState();
    const userId = loginUser?.userId;
    return this.userProfilesService
      .retrieveUserProfile_GetByUserId(userId)
      .pipe(
        tap((result: IUserProfileModel) => {
          const res = result as IUserProfile;
          ctx.setState({
            ...state,
            ...loginUser,
            ...res,
            country: '500',
            claims,
            profilePicture:
              'https://fastly.picsum.photos/id/91/3504/2336.jpg?hmac=tK6z7RReLgUlCuf4flDKeg57o6CUAbgklgLsGL0UowU',
            errors: null,
          });
          this.store.dispatch(new GetPicklists(ctx.getState().country));
          console.log(
            '----------------> userValues',
            this.store.selectSnapshot(PicklistsSelectors.userValues)
          );
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
    const model = {} as unknown as IUserProfileModel;

    ctx.setState({
      ...ctx.getState(),
      ...payload,
    });

    Object.assign(model, ctx.getState());
    const payloadTest = ctx.getState();
    return this.userProfilesService
      .updateUserProfile(ctx.getState().userId, payloadTest)
      .pipe(
        tap((result: IUserProfileModel) => {
          ctx.setState({
            ...ctx.getState(),
            ...result,
            errors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.setState({
            ...ctx.getState(),
            errors,
          });
          return of(errors);
        })
      );
  }

  @Action(LinkUserData)
  linkUserData(ctx: StateContext<IUserProfile>) {
    this.store.dispatch(new GetPicklists());
  }
}
