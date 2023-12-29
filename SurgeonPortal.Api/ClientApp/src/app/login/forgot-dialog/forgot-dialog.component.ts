import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { Store } from '@ngxs/store';
import { ForgotPassword, ForgotUsername } from 'src/app/state';
import { IForgotUsernameReadOnlyModel } from 'src/app/api/models/auth/forgot-username-read-only.model';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { IForgotPasswordReadOnlyModel } from 'src/app/api/models/auth/forgot-password-read-only.model';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

@UntilDestroy()
@Component({
  selector: 'abs-forgot-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    PasswordModule,
    ButtonModule,
  ],
  templateUrl: './forgot-dialog.component.html',
  styleUrls: ['./forgot-dialog.component.scss'],
})
export class ForgotDialogComponent {
  @Input() forgotType!: string | null;
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  usernameForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
  });

  passwordForm = new FormGroup({
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
    ]),
  });

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService
  ) {}

  onSubmit() {
    this.globalDialogService.showLoading();
    if (this.forgotType === 'password') {
      const model = {
        userName: this.passwordForm.get('username')?.value,
      };
      this._store
        .dispatch(new ForgotPassword(model as IForgotPasswordReadOnlyModel))
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          this.resetAndCloseDialog();
        });
    }

    if (this.forgotType === 'username') {
      const model = {
        email: this.usernameForm.get('email')?.value,
      };
      this._store
        .dispatch(new ForgotUsername(model as IForgotUsernameReadOnlyModel))
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          this.resetAndCloseDialog();
        });
    }
  }

  resetAndCloseDialog() {
    this.usernameForm.reset();
    this.passwordForm.reset();
    this.returnToLogin();
  }

  returnToLogin() {
    console.log('return to login');
    this.closeDialog.emit();
  }
}
