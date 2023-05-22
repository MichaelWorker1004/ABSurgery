import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { debounceTime, Observable, of, take } from 'rxjs';
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
  IPickListItem,
  PicklistsSelectors,
  PicklistsState,
} from '../state/picklists';
import {
  IEthnicityReadOnlyModel,
  IRaceReadOnlyModel,
  IStateReadOnlyModel,
} from '../api';

import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { SuccessFailModalComponent } from '../shared/components/success-fail-modal/success-fail-modal.component';

import '../../web-components';

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
  // TODO: [Joe] set up national provider identifier (NPI) report button

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

  // mailingStates$: Observable<IStateReadOnlyModel[] | undefined> = of([]);
  mailingStates: IStateReadOnlyModel[] = [];
  // birthStates$: Observable<IStateReadOnlyModel[] | undefined> = of([]);
  birthStates: IStateReadOnlyModel[] = [];

  user: IUserProfile | undefined;

  isEdit = true;

  // TODO: [Joe] this is eventually getting moved to a service for universal modals
  call!: any;

  userProfileForm: FormGroup = new FormGroup({
    absId: new FormControl('', []),
    bestLanguageId: new FormControl('', [Validators.required]),
    birthCity: new FormControl('', [Validators.required]),
    birthCountry: new FormControl('', [Validators.required]),
    birthDate: new FormControl('', [Validators.required]),
    birthState: new FormControl('', [Validators.required]),
    city: new FormControl('', [Validators.required]),
    country: new FormControl('', [Validators.required]),
    countryCitizenship: new FormControl('', [Validators.required]),
    displayName: new FormControl('', [Validators.required]),
    emailAddress: new FormControl('', []),
    ethnicity: new FormControl('', [Validators.required]),
    firstLanguageId: new FormControl('', [Validators.required]),
    firstName: new FormControl('', [Validators.required]),
    genderId: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    middleName: new FormControl('', [
      Validators.minLength(1),
      Validators.maxLength(1),
    ]),
    mobilePhoneNumber: new FormControl('', []),
    nPI: new FormControl('', []),
    officePhoneNumber: new FormControl('', [Validators.required]),
    profilePicture: new FormControl('', []),
    race: new FormControl('', [Validators.required]),
    receiveComms: new FormControl(false, [Validators.required]),
    state: new FormControl('', [Validators.required]),
    street1: new FormControl('', [Validators.required]),
    street2: new FormControl('', []),
    suffix: new FormControl('', []),
    userConfirmed: new FormControl(false, [Validators.requiredTrue]),
    zipCode: new FormControl('', [Validators.required]),
  });

  constructor(private _store: Store, private formBuilder: FormBuilder) {
    this.user$
      ?.pipe(debounceTime(300), untilDestroyed(this))
      .subscribe((user: IUserProfile) => {
        this.user = user;
        this._store.dispatch(new GetStateList(user.country)).subscribe();
        this.mailingStates = this._store.selectSnapshot(
          PicklistsSelectors.slices.states
        ) as IStateReadOnlyModel[];
        this._store.dispatch(new GetStateList(user.birthCountry));
        this.birthStates = this._store.selectSnapshot(
          PicklistsSelectors.slices.states
        ) as IStateReadOnlyModel[];
        this.userProfileForm.patchValue({ ...user });
        this.userProfileForm
          .get('birthDate')
          ?.setValue(new Date(user.birthDate));
      });

    this.userProfileForm.get('country')?.valueChanges.subscribe((value) => {
      this._store
        .dispatch(new GetStateList(value))
        .pipe(take(1))
        .subscribe(() => {
          this.mailingStates = this._store.selectSnapshot(
            PicklistsSelectors.slices.states
          ) as IStateReadOnlyModel[];
        });
    });

    this.userProfileForm
      .get('birthCountry')
      ?.valueChanges.subscribe((value) => {
        this._store
          .dispatch(new GetStateList(value))
          .pipe(take(1))
          .subscribe(() => {
            this.birthStates = this._store.selectSnapshot(
              PicklistsSelectors.slices.states
            ) as IStateReadOnlyModel[];
          });
      });
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
    this.call.showDialog = $event.show;
  }

  onSubmit() {
    // console.log('onSubmit');
    this._store.dispatch(new UpdateUserProfile(this.userProfileForm.value));
  }
}
