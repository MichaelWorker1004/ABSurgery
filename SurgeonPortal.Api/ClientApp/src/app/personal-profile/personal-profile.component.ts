import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { debounceTime, Observable, of } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Select, Store } from '@ngxs/store';

import { NgxMaskDirective } from 'ngx-mask';
import { NgxMaskPipe } from 'ngx-mask';
import { provideNgxMask } from 'ngx-mask';

import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { SuccessFailModalComponent } from '../shared/components/success-fail-modal/success-fail-modal.component';
import { UpdateUserProfile, UserProfileSelectors } from '../state';
import { IUserProfile } from '../state';
import { GetStateList } from '../state/picklists';
import { IPickListItem, PicklistsSelectors } from '../state/picklists';
import {
  ICountryReadOnlyModel,
  IEthnicityReadOnlyModel,
  IGenderReadOnlyModel,
  ILanguageReadOnlyModel,
  IRaceReadOnlyModel,
  IStateReadOnlyModel,
} from '../api';

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
  ],
  providers: [provideNgxMask()],
})
export class PersonalProfileComponent implements OnInit {
  // TODO: [Joe] set up input masks
  // TODO: [Joe] set up validation
  // TODO: [Joe] set up national provider identifier (NPI) report button

  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  @Select(PicklistsSelectors.slices.countries) countries$:
    | Observable<ICountryReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.ethnicities) ethnicities$:
    | Observable<IEthnicityReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.genders) genders$:
    | Observable<IGenderReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.languages) languages$:
    | Observable<ILanguageReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.races) races$:
    | Observable<IRaceReadOnlyModel[]>
    | undefined;

  @Select(PicklistsSelectors.slices.states) states$:
    | Observable<IStateReadOnlyModel[]>
    | undefined;

  mailingStates$: Observable<IStateReadOnlyModel[]> = of([]);

  isEdit = true;

  // TODO: [Joe] this is eventually getting moved to a service for universal modals
  call!: any;

  userProfileForm: FormGroup = new FormGroup({
    aBSId: new FormControl('', []),
    bestLanguageId: new FormControl(0, []),
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
    firstLanguageId: new FormControl(0, []),
    firstName: new FormControl('', [Validators.required]),
    genderId: new FormControl(0, []),
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

  constructor(private _store: Store, private formBuilder: FormBuilder) {
    this.user$
      ?.pipe(debounceTime(300), untilDestroyed(this))
      .subscribe((user: IUserProfile) => {
        this.userProfileForm.patchValue({ ...user });
        // this.userProfileForm.setValue('country', user.country);
        // console.log('-------------------->', this.userProfileForm);
        this.userProfileForm.patchValue({ country: user.country });
      });
  }

  ngOnInit() {
    // Country field
    this.userProfileForm
      .get('country')
      ?.valueChanges.pipe(debounceTime(300), untilDestroyed(this))
      .subscribe((value) => {
        // console.log('country', value);
        this._store.dispatch(new GetStateList(value));
        // const el: any = document.getElementById('country');
        // if (el) {
        //   el.value = value;
        // }
      });
    // Birth Country field
    this.userProfileForm
      .get('birthCountry')
      ?.valueChanges.pipe(debounceTime(300), untilDestroyed(this))
      .subscribe((value) => {
        // console.log('birthCountry', value);
        this._store.dispatch(new GetStateList(value));
      });
    // Citizenship Country field
    this.userProfileForm
      .get('countryCitizenship')
      ?.valueChanges.pipe(debounceTime(300), untilDestroyed(this))
      .subscribe((value) => {
        // console.log('countryCitizenship', value);
        this._store.dispatch(new GetStateList(value));
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

  // TODO: <Alan>Using this to record the checkbox state
  //  We will want to make this global if this hack has to be used
  //  There seems to currently be no elegant way to do this
  handleCheckbox($event: any) {
    const checkbox = $event.target;
    const formControl = $event.target.getAttribute('data-controlname');
    const patchObj: any = {};
    patchObj[formControl] = checkbox.checked;
    this.userProfileForm.patchValue(patchObj);
  }

  handleSelects($event: any) {
    const select = $event.target;
    const formControl = $event.target.getAttribute('data-controlname');
    const patchObj: any = {};
    patchObj[formControl] = select.value;
    // console.log('handleSelects', patchObj);
    this.userProfileForm.patchValue(patchObj);
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
