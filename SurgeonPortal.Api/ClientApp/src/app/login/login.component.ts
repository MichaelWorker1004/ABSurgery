import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Select, Store } from '@ngxs/store';

import { Login } from '../state/auth/auth.actions';
import { AuthSelectors } from '../state/auth/auth.selectors';
import { IError, IAuthCredentials } from '../api/services/auth/auth.service';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'abs-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
})
export class LoginComponent {
  loginForm = new FormGroup({
    emailAddress: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
  });

  @Select(AuthSelectors.getErrors) errors$?: Observable<IError> | undefined;

  constructor(private store: Store) {
    this.errors$?.pipe(
      tap((errors) => {
        // console.log('In the component', errors);
      })
    );
  }

  getErrors(error: IError) {
    let errorArray: string[] = [];
    const errors = error.errors as {
      EmailAddress: string[];
      Password: string[];
    };
    if (errors?.EmailAddress || errors?.Password) {
      errorArray = errorArray.concat(
        errors.EmailAddress ? errors.EmailAddress : [],
        errors.Password ? errors.Password : []
      );
    }
    return errorArray;
  }

  onSubmit() {
    this.store.dispatch(new Login(this.loginForm.value as IAuthCredentials));
  }
}
