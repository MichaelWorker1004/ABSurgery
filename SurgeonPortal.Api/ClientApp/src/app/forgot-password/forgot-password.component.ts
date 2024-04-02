/* eslint-disable prettier/prettier */
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ForgotDialogComponent } from '../login/forgot-dialog/forgot-dialog.component';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { validatePassword, matchFields } from '../shared/validators/validators';
import packageInfo from '../../../package.json';
import { Store } from '@ngxs/store';
import { ActivatedRoute, Router } from '@angular/router';
import { IResetForgotPasswordReadOnlyModel } from '../api/models/auth/reset-forgot-password-read-only-model';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { ResetForgotPassword } from '../state';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'abs-forgot-password',
  standalone: true,
  imports: [
    CommonModule,
    CommonModule,
    TranslateModule,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    PasswordModule,
    ButtonModule,
    DialogModule,
    ForgotDialogComponent,
    ModalComponent,
  ],
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
})
export class ForgotPasswordComponent {
  now = new Date();
  isBusy = false;
  version = packageInfo.buildId;
  guid!: string;
  /**
   * The password reset form
   */
  passwordResetForm = new FormGroup(
    {
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
  constructor(
    private _store: Store,
    private _router: Router,
    private _route: ActivatedRoute,
    private globalDialogService: GlobalDialogService
  ) {
    this._route.queryParams.subscribe((params) => {
      this.guid = params['guid'];
    });
  }

  onSubmit() {
    this.globalDialogService.showLoading();
    const form = this.passwordResetForm.getRawValue();
    const model = {
      newPassword: form.newPassword,
      resetGUID: this.guid,
    };

    this._store
      .dispatch(
        new ResetForgotPassword(model as IResetForgotPasswordReadOnlyModel)
      )
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this._router.navigate(['/login']);
      });
  }

  navigateToLogin() {
    this._router.navigate(['/login']);
  }
}
