import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  Inject,
  Input,
} from '@angular/core';
import { CommonModule, DOCUMENT } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { Select, Store } from '@ngxs/store';
import {
  AuthSelectors,
  Login,
  IError,
  IAuthCredentials,
  ResetPassword,
  Logout,
} from '../state';
import { ClearAuthErrors } from '../state';

import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { ActivatedRoute, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { DialogModule } from 'primeng/dialog';
import { matchFields, validatePassword } from '../shared/validators/validators';

@UntilDestroy()
@Component({
  selector: 'abs-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    PasswordModule,
    ButtonModule,
    DialogModule,
  ],
})
export class LoginComponent {
  /**
   * The version of the application
   */
  @Input() version = '';

  now = new Date();
  @Select(AuthSelectors.slices.errors) errors$?: Observable<IError> | undefined;
  @Select(AuthSelectors.slices.isAuthenticated) isAuthenticated$?:
    | Observable<boolean>
    | undefined;
  @Select(AuthSelectors.slices.isBusy) isBusy$?:
    | Observable<boolean>
    | undefined;
  @Select(AuthSelectors.slices.passwordResetComplete) passwordResetComplete$?:
    | Observable<boolean>
    | undefined;

  /**
   * The login form
   */
  loginForm = new FormGroup({
    userName: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
  });
  /**
   * The password reset form
   */
  passwordResetForm = new FormGroup(
    {
      currentPassword: new FormControl('', [
        validatePassword(),
        Validators.required,
      ]),
      newPassword: new FormControl('', [
        validatePassword(),
        Validators.required,
      ]),
      confirmPassword: new FormControl('', [
        validatePassword(),
        Validators.required,
      ]),
    },
    {
      validators: [matchFields('newPassword', 'confirmPassword')],
    }
  );
  /**
   * The clear errors action
   */
  clearErrorAction = new ClearAuthErrors();
  /**
   * Is the component shows the busy state either for login or password reset
   */
  isBusy = false;
  /**
   * The password reset dialog open
   */
  isPasswordReset = false;
  /**
   * The password reset is complete
   */
  passwordResetComplete = false;
  constructor(
    private store: Store,
    private router: Router,
    private route: ActivatedRoute,
    @Inject(DOCUMENT) private document: Document
  ) {
    this.clearErrors();
    this.isBusy$?.pipe(untilDestroyed(this))?.subscribe((isBusy) => {
      this.isBusy = isBusy;
    });
    this.isAuthenticated$?.pipe(untilDestroyed(this)).subscribe((isAuthed) => {
      this.isPasswordReset = this.store.selectSnapshot(
        AuthSelectors.slices.isPasswordReset
      );
      if (isAuthed && !this.isPasswordReset) {
        this.router.navigate([
          this.route.snapshot.queryParams['returnUrl'] ?? '/',
        ]);
      }
    });
    this.passwordResetComplete$?.pipe(untilDestroyed(this)).subscribe((val) => {
      this.passwordResetComplete = val;
    });
  }

  clearErrors() {
    this.store.dispatch(this.clearErrorAction);
  }

  getErrors(error: IError) {
    let errorArray: string[] = [];
    const errors = error.errors as {
      userName: string[];
      Password: string[];
    };
    if (errors?.userName || errors?.Password) {
      errorArray = errorArray.concat(
        errors.userName ? errors.userName : [],
        errors.Password ? errors.Password : []
      );
    }
    return errorArray;
  }

  onSubmit() {
    const loginPayload = {
      ...this.loginForm.value,
    } as unknown as IAuthCredentials;
    this.store.dispatch(new Login(loginPayload as IAuthCredentials));
  }

  onSubmitPasswordReset() {
    const payload: { oldPassword: string; newPassword: string } = {
      oldPassword: this.passwordResetForm.value.currentPassword
        ? this.passwordResetForm.value.currentPassword
        : '',
      newPassword: this.passwordResetForm.value.newPassword
        ? this.passwordResetForm.value.newPassword
        : '',
    };
    this.store.dispatch(new ResetPassword(payload));
  }

  completePasswordReset() {
    this.passwordResetForm.reset();
    this.loginForm.reset();
    this.store.dispatch(new Logout());
  }

  navigateToAbs(): void {
    this.document.location.href = 'https://www.absurgery.org/login.jsp';
  }
}
