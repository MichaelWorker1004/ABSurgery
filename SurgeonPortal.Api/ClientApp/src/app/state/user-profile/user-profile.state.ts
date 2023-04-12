import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormControl, FormGroup } from '@angular/forms';
import { catchError, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { IAppUserReadOnlyModel, IUserProfileModel } from '../../api';
import { IFormErrors } from '../../shared/common';
import { UserProfilesService } from '../../api';
import { GetUserProfile, UpdateUserProfile } from './user-profile.actions';

export interface IUserProfile extends IUserProfileModel, IAppUserReadOnlyModel {
  claims: string[];
  profilePicture: string; // TODO: <API>This is not generated
  errors?: IFormErrors | null;
}

// TODO: work out how to implement this in the store and use it in the form
// export interface IUserProfileForm extends FormGroup {
//   value: IUserProfileModel;
//   controls: {
//     userProfileId: FormControl;
//     userId: FormControl;
//     firstName: FormControl;
//     middleName: FormControl;
//     lastName: FormControl;
//     suffix: FormControl;
//     displayName: FormControl;
//     officePhoneNumber: FormControl;
//     mobilePhoneNumber: FormControl;
//     birthCity: FormControl;
//     birthState: FormControl;
//     birthCountry: FormControl;
//     countryCitizenship: FormControl;
//     aBSId: FormControl;
//     certificationStatusId: FormControl;
//     nPI: FormControl;
//     genderId: FormControl;
//     birthDate: FormControl;
//     race: FormControl;
//     ethnicity: FormControl;
//     firstLanguageId: FormControl;
//     bestLanguageId: FormControl;
//     receiveComms: FormControl;
//     userConfirmed: FormControl;
//     userConfirmedDate: FormControl;
//     createdByUserId: FormControl;
//     createdAtUtc: FormControl;
//     lastUpdatedAtUtc: FormControl;
//     lastUpdatedByUserId: FormControl;
//     street1: FormControl;
//     street2: FormControl;
//     city: FormControl;
//     state: FormControl;
//     zipCode: FormControl;
//     country: FormControl;
//     profilePicture: FormControl;
//   };
// }

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
    aBSId: '',
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
  constructor(private userProfilesService: UserProfilesService) {}

  @Action(GetUserProfile)
  getUserProfile(
    ctx: StateContext<IUserProfile>,
    { loginUser, claims }: GetUserProfile
  ) {
    const state = ctx.getState();
    const userId = loginUser?.userId ? loginUser.userId : -1;
    return this.userProfilesService
      .retrieveUserProfile_GetByUserId(userId)
      .pipe(
        tap((result: IUserProfileModel) => {
          const res = result as IUserProfile;
          ctx.setState({
            ...state,
            ...loginUser,
            ...res,
            claims,
            profilePicture:
              'https://fastly.picsum.photos/id/91/3504/2336.jpg?hmac=tK6z7RReLgUlCuf4flDKeg57o6CUAbgklgLsGL0UowU',
            errors: null,
          });
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
    // const state = ctx.getState();
    const model = {} as unknown as IUserProfileModel;

    ctx.setState({
      ...ctx.getState(),
      ...payload,
    });

    Object.assign(model, ctx.getState());
    const payloadTest = ctx.getState();
    // const payloadTest = { model };
    // console.log('------------>', payload, ctx.getState());
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
          // ctx.patchState({ errors });
          ctx.setState({
            ...ctx.getState(),
            errors,
          });
          return of(errors);
        })
      );
  }
}
