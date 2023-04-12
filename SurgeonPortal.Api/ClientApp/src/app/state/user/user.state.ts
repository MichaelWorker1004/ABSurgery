// import { State, StateToken, Store } from '@ngxs/store';
// import { Injectable } from '@angular/core';
// import { IUserProfileModel, IAppUserReadOnlyModel } from '../../api';
// import { IFormErrors } from '../../shared/common';
//
// export interface IUser extends IUserProfileModel, IAppUserReadOnlyModel {
//   errors?: IFormErrors | null;
// }
//
// const USER_STATE_TOKEN = new StateToken<IUser>('user');
// @State<IUser>({
//   name: USER_STATE_TOKEN,
//   defaults: {
//     userId: null,
//     userProfileId: null,
//     fullName: null,
//     firstName: null,
//     middleName: null,
//     lastName: null,
//     displayName: null,
//     title: null,
//     suffix: null,
//     officePhoneNumber: null,
//     mobilePhoneNumber: null,
//     birthCity: null,
//     birthState: null,
//     birthCountry: null,
//     countryCitizenship: null,
//     aBSId: null,
//     certificationStatusId: null,
//     nPI: null,
//     genderId: null,
//     birthDate: null,
//     race: null,
//     ethnicity: null,
//     firstLanguageId: null,
//     bestLanguageId: null,
//     receiveComms: null,
//     userConfirmed: null,
//     userConfirmedDate: null,
//     createdByUserId: null,
//     createdAtUtc: null,
//     lastUpdatedAtUtc: null,
//     lastUpdatedByUserId: null,
//     street1: null,
//     street2: null,
//     city: null,
//     state: null,
//     zipCode: null,
//     country: null,
//   },
// })
// @Injectable()
// export class UserState {
//   constructor(private store: Store) {}
// }
