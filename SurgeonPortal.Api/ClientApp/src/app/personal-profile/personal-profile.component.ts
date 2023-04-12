import { CommonModule, NgIf } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnDestroy } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Select, Store } from '@ngxs/store';

import { Observable, Subscription } from 'rxjs';
import { NgxMaskDirective } from 'ngx-mask';
import { NgxMaskPipe } from 'ngx-mask';
import { provideNgxMask } from 'ngx-mask';

import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { SuccessFailModalComponent } from '../shared/components/success-fail-modal/success-fail-modal.component';
import { UpdateUserProfile, UserProfileSelectors } from '../state';
import { IUserProfile } from '../state';

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
export class PersonalProfileComponent implements OnDestroy {
  // TODO: [Joe] set up input masks
  // TODO: [Joe] set up validation
  // TODO: [Joe] set up national provider identifier (NPI) report button

  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  stateValue = '';
  userSubscription: Subscription | undefined;
  isEdit = true;

  // TODO: [Joe] this is eventually getting moved to a service for universal modals
  call!: any;

  // userProfileForm: IUserProfileForm;
  userProfileForm: FormGroup = new FormGroup({
    aBSId: new FormControl(0, []),
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

  constructor(private store: Store, private formBuilder: FormBuilder) {
    this.userSubscription = this.user$?.subscribe((user) => {
      this.userProfileForm.patchValue({ ...user });
    });
  }

  ngOnDestroy(): void {
    this.userSubscription?.unsubscribe();
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
    console.log('handleSelects', patchObj);
    this.userProfileForm.patchValue(patchObj);
  }

  toggleDialog($event: any) {
    console.log('toggleDialog', $event.show);
    this.call.showDialog = $event.show;
  }

  onSubmit() {
    console.log('onSubmit');
    this.store.dispatch(new UpdateUserProfile(this.userProfileForm.value));
  }
}
