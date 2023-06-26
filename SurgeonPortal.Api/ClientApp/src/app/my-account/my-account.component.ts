import { Component, CUSTOM_ELEMENTS_SCHEMA, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Select, Store } from '@ngxs/store';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';

import { matchFields, validatePassword } from '../shared/validators/validators';
import {
  IUserCredential,
  MyAccountSelectors,
  SaveMyAccountChanges,
  UserProfileSelectors,
  IUserProfile,
} from '../state';
import { ClearErrors } from '../state';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { FormErrorsComponent } from '../shared/components/form-errors/form-errors.component';

import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { GlobalDialogService } from '../shared/services/global-dialog.service';

@Component({
  selector: 'abs-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ProfileHeaderComponent,
    FormErrorsComponent,
    InputTextModule,
    PasswordModule,
    CheckboxModule,
    ButtonModule,
  ],
})
export class MyAccountComponent implements OnDestroy {
  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile | null>
    | undefined;
  @Select(MyAccountSelectors.errors) errors$: Observable<any> | undefined;

  clearErrors = new ClearErrors();

  userSub: Subscription | undefined;
  user: IUserProfile | null = null;

  isEdit = false;
  profilePicture =
    'https://fastly.picsum.photos/id/91/3504/2336.jpg?hmac=tK6z7RReLgUlCuf4flDKeg57o6CUAbgklgLsGL0UowU';

  hasUnsavedChanges = false;
  isSubmitted = false;

  myAccountForm: FormGroup = new FormGroup(
    {
      emailAddress: new FormControl('', [Validators.email]),
      confirmEmailAddress: new FormControl('', [Validators.email]),
      password: new FormControl('', [validatePassword()]),
      confirmPassword: new FormControl('', [validatePassword()]),
      mailingList: new FormControl(true),
    },
    {
      validators: [
        matchFields('emailAddress', 'confirmEmailAddress'),
        matchFields('password', 'confirmPassword'),
      ],
    }
  );
  constructor(
    private store: Store,
    public globalDialogService: GlobalDialogService
  ) {
    this.userSub = this.user$?.subscribe((user) => {
      if (user) {
        this.user = user;
        this.myAccountForm.patchValue({
          emailAddress: this.user?.emailAddress,
          confirmEmailAddress: this.user?.emailAddress,
        });
      }
    });

    this.myAccountForm.valueChanges.subscribe(() => {
      const isDirty = this.myAccountForm.dirty;
      if (isDirty && !this.isSubmitted) {
        this.hasUnsavedChanges = true;
      } else {
        this.hasUnsavedChanges = false;
      }
    });
  }

  ngOnDestroy() {
    this.userSub?.unsubscribe();
  }

  getErrors(error: object) {
    return Object.values(error);
  }

  resetForm($event: Event) {
    $event.preventDefault();
    this.myAccountForm.reset();
    this.myAccountForm.patchValue({
      emailAddress: this.user?.emailAddress,
      confirmEmailAddress: this.user?.emailAddress,
    });
    this.isEdit = false;
  }

  selectAll($event: Event) {
    const target = $event.target as HTMLInputElement;
    target.select();
  }
  onSubmit() {
    // submit actions
    const emailAddress = this.myAccountForm.value.emailAddress
      ? this.myAccountForm.value.emailAddress
      : null;
    const password = this.myAccountForm.value.password;
    const userCreds: IUserCredential = {
      emailAddress,
      password,
    };
    this.store.dispatch(new SaveMyAccountChanges(userCreds));
    this.isSubmitted = true;
  }
}
