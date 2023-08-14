import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { debounceTime, Observable, take } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { NgxMaskDirective } from 'ngx-mask';
import { NgxMaskPipe } from 'ngx-mask';
import { provideNgxMask } from 'ngx-mask';
import {
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
import { GlobalDialogService } from '../shared/services/global-dialog.service';

interface IDisplayUserProfile extends IUserProfile {
  gender: string;
  countryDisplay: string;
  birthCountryDisplay: string;
  citizenshipCountryDisplay: string;
  firstLanguage: string;
  bestLanguage: string;
  ethnicityDisplay: string;
  raceDisplay: string;
}

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
export class PersonalProfileComponent implements OnInit {
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

  mailingStates: IStateReadOnlyModel[] = [];
  birthStates: IStateReadOnlyModel[] = [];

  user!: IDisplayUserProfile;

  isEdit = false;

  userProfileForm: FormGroup = new FormGroup({
    absId: new FormControl('', []), //readonly
    npi: new FormControl('', []), //readonly
    emailAddress: new FormControl('', []), //readonly
    birthState: new FormControl('', []), //sometimes no options
    state: new FormControl('', []), //sometimes no options
    street2: new FormControl('', []), //sometimes no valid value
    suffix: new FormControl('', []), //sometimes no valid value
    mobilePhoneNumber: new FormControl('', []), // typically not required in forms
    middleName: new FormControl('', [
      Validators.minLength(1),
      Validators.maxLength(1),
    ]), // typically not required in forms
    profilePicture: new FormControl('', []), //currently no input
    bestLanguageId: new FormControl('', [Validators.required]),
    birthCity: new FormControl('', [Validators.required]),
    birthCountry: new FormControl('', [Validators.required]),
    birthDate: new FormControl(new Date(), [Validators.required]),
    city: new FormControl('', [Validators.required]),
    country: new FormControl('', [Validators.required]),
    countryCitizenship: new FormControl('', [Validators.required]),
    displayName: new FormControl('', [Validators.required]),
    ethnicity: new FormControl('', [Validators.required]),
    firstLanguageId: new FormControl('', [Validators.required]),
    firstName: new FormControl('', [Validators.required]),
    genderId: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    officePhoneNumber: new FormControl('', [Validators.required]),
    race: new FormControl('', [Validators.required]),
    receiveComms: new FormControl(false, [Validators.required]),
    street1: new FormControl('', [Validators.required]),
    userConfirmed: new FormControl(false, [Validators.requiredTrue]),
    zipCode: new FormControl('', [Validators.required]),
  });

  hasUnsavedChanges = false;
  isSubmitted = false;

  constructor(
    private _store: Store,
    public globalDialogService: GlobalDialogService
  ) {
    this.userProfileForm.controls['state'].disable();
    this.userProfileForm.controls['birthState'].disable();
    this.user$
      ?.pipe(debounceTime(300), untilDestroyed(this))
      .subscribe((user: IUserProfile) => {
        if (!user) {
          this.isEdit = true;
        }
        const languages = this._store.selectSnapshot(
          PicklistsSelectors.slices.languages
        ) as IPickListItem[];

        const genders = this._store.selectSnapshot(
          PicklistsSelectors.slices.genders
        ) as IPickListItem[];

        //countries
        const countries = this._store.selectSnapshot(
          PicklistsSelectors.slices.countries
        ) as IPickListItem[];

        //ethnicities
        const ethnicities = this._store.selectSnapshot(
          PicklistsSelectors.slices.ethnicities
        ) as IEthnicityReadOnlyModel[];

        //races
        const races = this._store.selectSnapshot(
          PicklistsSelectors.slices.races
        ) as IRaceReadOnlyModel[];

        this.user = {
          ...user,
          gender:
            genders.find((x) => x.itemValue === user?.genderId)
              ?.itemDescription || '',
          countryDisplay:
            countries.find((x) => x.itemValue === user?.country)
              ?.itemDescription || '',
          birthCountryDisplay:
            countries.find((x) => x.itemValue === user?.birthCountry)
              ?.itemDescription || '',
          citizenshipCountryDisplay:
            countries.find((x) => x.itemValue === user?.countryCitizenship)
              ?.itemDescription || '',
          firstLanguage:
            languages.find((x) => x.itemValue === user?.firstLanguageId)
              ?.itemDescription || '',
          bestLanguage:
            languages.find((x) => x.itemValue === user?.bestLanguageId)
              ?.itemDescription || '',
          ethnicityDisplay:
            ethnicities.find((x) => x.itemValue === user?.ethnicity)
              ?.itemDescription || '',
          raceDisplay:
            races.find((x) => x.itemValue === user?.race)?.itemDescription ||
            '',
        } as IDisplayUserProfile;

        this._store.dispatch(new GetStateList(user.country)).subscribe();
        this.mailingStates = this._store.selectSnapshot(
          PicklistsSelectors.slices.states
        ) as IStateReadOnlyModel[];
        this._store.dispatch(new GetStateList(user.birthCountry));
        this.birthStates = this._store.selectSnapshot(
          PicklistsSelectors.slices.states
        ) as IStateReadOnlyModel[];
        this.userProfileForm.patchValue({ ...user });
        const date = new Date(Date.parse(user?.birthDate));
        if (!isNaN(date as unknown as number)) {
          this.userProfileForm.get('birthDate')?.patchValue(date);
        }
      });

    this.userProfileForm
      .get('country')
      ?.valueChanges.pipe(debounceTime(300), untilDestroyed(this))
      .subscribe((value) => {
        this._store
          .dispatch(new GetStateList(value))
          .pipe(take(1))
          .subscribe(() => {
            this.mailingStates = this._store.selectSnapshot(
              PicklistsSelectors.slices.states
            ) as IStateReadOnlyModel[];
            if (this.mailingStates.length > 0) {
              this.userProfileForm.controls['state'].enable();
            } else {
              this.userProfileForm.controls['state'].disable();
            }
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
            if (this.birthStates.length > 0) {
              this.userProfileForm.controls['birthState'].enable();
            } else {
              this.userProfileForm.controls['birthState'].disable();
            }
          });
      });
  }

  ngOnInit(): void {
    this.userProfileForm.valueChanges.subscribe(() => {
      const isDirty = this.userProfileForm.dirty;
      if (isDirty && !this.isSubmitted) {
        this.hasUnsavedChanges = true;
      } else {
        this.hasUnsavedChanges = false;
      }
    });
  }

  resetFormDefaults() {
    this.userProfileForm.patchValue({ ...this.user });
    if (this.user?.birthDate) {
      const date = new Date(Date.parse(this.user.birthDate));
      if (!isNaN(date as unknown as number)) {
        this.userProfileForm.get('birthDate')?.patchValue(date);
      }
    }
  }

  toggleEdit() {
    this.isEdit = !this.isEdit;
    this.resetFormDefaults();
    this.hasUnsavedChanges = false;
  }

  onSubmit() {
    this.isSubmitted = true;

    this.isEdit = false;
    this.hasUnsavedChanges = false;

    const model = this.userProfileForm.value;
    model.birthDate = new Date(model.birthDate).toISOString();

    this._store.dispatch(new UpdateUserProfile(model));
  }
}
