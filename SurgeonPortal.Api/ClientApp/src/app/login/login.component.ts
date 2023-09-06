import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  Input,
  OnInit,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Select, Store } from '@ngxs/store';
import { AuthSelectors, Login } from '../state';
import { IError, IAuthCredentials } from '../api';
import { ClearAuthErrors } from '../state';

import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';

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
  ],
})
export class LoginComponent implements OnInit {
  @Input() version = '';

  now = new Date();

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

  clearErrorAction = new ClearAuthErrors();

  @Select(AuthSelectors.getErrors) errors$?: Observable<IError> | undefined;

  constructor(private store: Store) {}
  ngOnInit(): void {
    this.clearErrors();
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
    this.store.dispatch(new Login(this.loginForm.value as IAuthCredentials));
  }
}
