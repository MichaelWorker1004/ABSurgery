import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { debounceTime, Observable, of } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { NgxMaskDirective } from 'ngx-mask';
import { NgxMaskPipe } from 'ngx-mask';
import { provideNgxMask } from 'ngx-mask';

import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Select, Store } from '@ngxs/store';

import { UpdateUserProfile, UserProfileSelectors } from '../state';
import { IUserProfile } from '../state';
import {
  GetStateList,
  IPicklist,
  IPickListItem,
  PicklistsSelectors,
} from '../state/picklists';
import {
  IEthnicityReadOnlyModel,
  IGenderReadOnlyModel,
  ILanguageReadOnlyModel,
  IRaceReadOnlyModel,
  IStateReadOnlyModel,
} from '../api';

import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { SuccessFailModalComponent } from '../shared/components/success-fail-modal/success-fail-modal.component';

import '../../web-components';
import { tap } from 'rxjs/operators';

import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputMaskModule } from 'primeng/inputmask';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';

@UntilDestroy()
@Component({
  selector: 'abs-personal-profile',
  templateUrl: './personal-profile.component.html',
  styleUrls: ['./personal-profile.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    ProfileHeaderComponent,
    SuccessFailModalComponent,
    NgxMaskDirective,
    NgxMaskPipe,

    InputTextModule,
    DropdownModule,
    InputMaskModule,
    CalendarModule,
    CheckboxModule,
  ],
  providers: [provideNgxMask()],
})
export class PersonalProfileComponent {
  // TODO: [Joe] set up input masks
  // TODO: [Joe] set up validation
  // TODO: [Joe] set up national provider identifier (NPI) report button
  testStates = [
    { itemDescription: 'Alabama', itemValue: 'AL' },
    { itemDescription: 'Alaska', itemValue: 'AK' },
    { itemDescription: 'Arizona', itemValue: 'AZ' },
    { itemDescription: 'Arkansas', itemValue: 'AR' },
    { itemDescription: 'California', itemValue: 'CA' },
  ];

  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;
  @Select(PicklistsSelectors.userPicklistValues) userPicklistValues$:
    | Observable<IPickListItem[]>
    | undefined;
  @Select(PicklistsSelectors.slices.countries) countries$:
    | Observable<IPickListItem[]>
    | undefined;
  @Select(PicklistsSelectors.slices.ethnicities) ethnicities$:
    | Observable<IEthnicityReadOnlyModel[]>
    | undefined;
  @Select(PicklistsSelectors.slices.genders) genders$:
    | Observable<IPickListItem[]>
    | undefined;
  @Select(PicklistsSelectors.slices.languages) languages$:
    | Observable<IPickListItem[]>
    | undefined;
  @Select(PicklistsSelectors.slices.races) races$:
    | Observable<IRaceReadOnlyModel[]>
    | undefined;

  mailingStates$: Observable<IStateReadOnlyModel[] | undefined> = of([]);
  birthStates$: Observable<IStateReadOnlyModel[] | undefined> = of([]);

  user: IUserProfile | undefined;

  isEdit = true;

  // TODO: [Joe] this is eventually getting moved to a service for universal modals
  call!: any;

  userProfileForm: FormGroup = new FormGroup({
    absId: new FormControl('', []),
    bestLanguageId: new FormControl('', []),
    birthCity: new FormControl('', []),
    birthCountry: new FormControl('', []),
    birthDate: new FormControl('', []),
    birthState: new FormControl('', []),
    city: new FormControl('', []),
    country: new FormControl('', []),
    countryCitizenship: new FormControl('', []),
    displayName: new FormControl('', [Validators.required]),
    emailAddress: new FormControl('', []),
    ethnicity: new FormControl('', []),
    firstLanguageId: new FormControl('', []),
    firstName: new FormControl('', [Validators.required]),
    genderId: new FormControl('', []),
    lastName: new FormControl('', [Validators.required]),
    middleName: new FormControl('', [
      Validators.minLength(1),
      Validators.maxLength(1),
    ]),
    mobilePhoneNumber: new FormControl('', []),
    nPI: new FormControl('', []),
    officePhoneNumber: new FormControl('', []),
    profilePicture: new FormControl('', []),
    race: new FormControl('', []),
    receiveComms: new FormControl(false, []),
    state: new FormControl('', []),
    street1: new FormControl('', []),
    street2: new FormControl('', []),
    suffix: new FormControl('', []),
    userConfirmed: new FormControl(false, [Validators.requiredTrue]),
    zipCode: new FormControl('', []),
  });

  // TODO: [Joe] we need to include logic to update the mailingStates and birthStates when the respective country values are changed

  constructor(private _store: Store, private formBuilder: FormBuilder) {
    this.user$
      ?.pipe(debounceTime(300), untilDestroyed(this))
      .subscribe((user: IUserProfile) => {
        this.user = user;

        if (user.country) {
          this._store.dispatch(new GetStateList(user.country));
        } else {
          // TODO: [Joe] this is a hack to get the states to load on the first load if the user has no country
          this._store.dispatch(new GetStateList('500'));
        }
        this.mailingStates$ = of(
          this._store.selectSnapshot(PicklistsSelectors.slices.states)
        );

        if (user.birthCountry) {
          this._store.dispatch(new GetStateList(user.birthCountry));
        } else {
          // TODO: [Joe] this is a hack to get the states to load on the first load if the user has no country
          this._store.dispatch(new GetStateList('500'));
        }
        this._store.dispatch(new GetStateList(user.birthCountry));
        this.birthStates$ = of(
          this._store.selectSnapshot(PicklistsSelectors.slices.states)
        );
        this.userProfileForm.patchValue({ ...user });
      });

    // this.userProfileForm.valueChanges.pipe().subscribe((value) => {
    //   console.log('valueChanges', value);
    // });
  }

  save() {
    this.call = {
      title: 'Success',
      message: 'Your profile has been updated.',
      isSuccess: true,
      showDialog: true,
    };
    this.toggleDialog(true);

    this.isEdit = false;
  }

  toggleDialog($event: any) {
    // console.log('toggleDialog', $event.show);
    this.call.showDialog = $event.show;
  }

  trackByFn(
    index: number,
    item: IPickListItem
  ): string | number | null | undefined {
    return item.itemValue;
  }

  onSubmit() {
    // console.log('onSubmit');
    this._store.dispatch(new UpdateUserProfile(this.userProfileForm.value));
  }
}
