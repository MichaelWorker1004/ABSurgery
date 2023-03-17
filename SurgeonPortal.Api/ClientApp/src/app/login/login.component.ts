import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Store } from '@ngxs/store';

import { Login } from '../state/auth/ngxs-auth.model';
import { IUserCredentialModel } from '../api/models/users/user-credential.model';

@Component({
  selector: 'abs-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
  imports: [FormsModule],
})
export class LoginComponent {
  _username = '';
  _password = '';
  isValid = false;

  get username(): string {
    return this._username;
  }
  set username(_username: string) {
    this._username = _username;
    this.validate();
  }

  get password(): string {
    return this._password;
  }
  set password(_password: string) {
    this._password = _password;
    this.validate();
  }

  constructor(private store: Store) {}

  validate() {
    if (this._username.length > 6 && this._password.length > 8) {
      this.isValid = true;
    } else {
      this.isValid = false;
    }
  }

  login($event: Event) {
    if (this.username.length > 5 && this.password.length > 8) {
      this.store.dispatch(
        new Login({
          emailAddress: this.username,
          password: this.password,
        } as IUserCredentialModel)
      );
    }
  }
}
