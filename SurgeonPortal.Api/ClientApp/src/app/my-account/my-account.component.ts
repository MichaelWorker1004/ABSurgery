/* eslint-disable prettier/prettier */
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnDestroy } from '@angular/core';
import { Observable, Subscription, take } from 'rxjs';
import { Select, Store } from '@ngxs/store';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';

import { matchFields, validatePassword } from '../shared/validators/validators';
import {
  IUserCredential,
  MyAccountSelectors,
  SaveMyAccountChanges,
  UserProfileSelectors,
  IUserProfile,
  UpdateUserProfile,
} from '../state';
import { ClearErrors } from '../state';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';
import { FormErrorsComponent } from '../shared/components/form-errors/form-errors.component';

import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import {
  CloseApplication,
  SetUnsavedChanges,
} from '../state/application/application.actions';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
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
    TranslateModule,
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

  isSubmitted = false;

  myAccountFormEmail: FormGroup = new FormGroup(
    {
      emailAddress: new FormControl('', [Validators.email]),
      confirmEmailAddress: new FormControl('', [Validators.email]),
      mailingList: new FormControl(true),
    },
    {
      validators: [
        matchFields('emailAddress', 'confirmEmailAddress'),
      ],
    }
  );

  myAccountFormPassword: FormGroup = new FormGroup(
    {
      password: new FormControl('', [validatePassword()]),
      confirmPassword: new FormControl('', [validatePassword()]),
      mailingList: new FormControl(true),
    },
    {
      validators: [
        matchFields('password', 'confirmPassword'),
      ],
    }
  );


  constructor(
    private store: Store,
    public globalDialogService: GlobalDialogService
  ) {
    this.store.dispatch(new SetUnsavedChanges(false));
    this.fetchUser();

    this.myAccountFormEmail.valueChanges.pipe(untilDestroyed(this)).subscribe(() => {
      const isDirty = this.myAccountFormEmail.dirty;
      this.store.dispatch(new SetUnsavedChanges(isDirty && !this.isSubmitted));
    });

    this.myAccountFormPassword.valueChanges.pipe(untilDestroyed(this)).subscribe(() => {
      const isDirty = this.myAccountFormPassword.dirty;
      this.store.dispatch(new SetUnsavedChanges(isDirty && !this.isSubmitted));
    });
  }

  fetchUser() {
    this.userSub = this.user$?.pipe(untilDestroyed(this)).subscribe((user) => {
      if (user) {
        this.user = user;
        this.myAccountFormEmail.patchValue({
          emailAddress: this.user?.emailAddress,
          confirmEmailAddress: this.user?.emailAddress,
        });
      }
    });
  }

  ngOnDestroy() {
    this.userSub?.unsubscribe();
    this.store.dispatch(new SetUnsavedChanges(false));
  }

  getErrors(error: object) {
    return Object.values(error);
  }

  resetForm($event: Event) {
    $event.preventDefault();
    this.myAccountFormEmail.reset();
    this.myAccountFormEmail.patchValue({
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
    const emailAddress = this.myAccountFormEmail.value.emailAddress
      ? this.myAccountFormEmail.value.emailAddress
      : null;
    const password = this.myAccountFormPassword.value.password;
    const userCreds: IUserCredential = {
      userId: this.user?.userId || null,
      emailAddress,
      password,
    };

    this.store
      .dispatch(new SaveMyAccountChanges(userCreds))
      .pipe(take(1))
      .subscribe(() => {
        this.isSubmitted = true;
        this.isEdit = false;
        this.store
          .dispatch(new SetUnsavedChanges(false));
      });

    this.updateUserState();
  }

  updateUserState() {
    const copyUser = Object.assign({}, this.user);
    copyUser.emailAddress = this.myAccountFormEmail.value.emailAddress;

    this.store.dispatch(new UpdateUserProfile(copyUser));
  }

}
