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
import { AuthSelectors } from '../state/auth/auth.selectors';
import { IUser } from '../api/services/auth/auth.service';
import { matchFields, validatePassword } from '../shared/validators/validators';
import { SaveMyAccountChanges } from '../state/my-account/my-account.actions';
import {
  IUserCredential,
  MyAccountState,
} from '../state/my-account/my-account.state';
import { ProfileHeaderComponent } from '../shared/components/profile-header/profile-header.component';

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
  ],
})
export class MyAccountComponent implements OnDestroy {
  @Select(AuthSelectors.getUser) user$: Observable<IUser | null> | undefined;
  @Select(MyAccountState.errors) errors$: Observable<any> | undefined;

  userSub: Subscription | undefined;
  user: IUser | null = null;

  isEdit = false;
  profilePicture =
    'https://fastly.picsum.photos/id/91/3504/2336.jpg?hmac=tK6z7RReLgUlCuf4flDKeg57o6CUAbgklgLsGL0UowU';

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
  constructor(private store: Store) {
    this.userSub = this.user$?.subscribe((user) => {
      if (user) {
        this.user = user;
        this.myAccountForm.patchValue({
          emailAddress: this.user?.emailAddress,
          confirmEmailAddress: this.user?.emailAddress,
        });
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
  }
}
