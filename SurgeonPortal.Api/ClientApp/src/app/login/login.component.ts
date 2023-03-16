import { Component, CUSTOM_ELEMENTS_SCHEMA, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { Store } from '@ngxs/store';
import { Login } from '../state/auth/ngxs-auth.model';
import { IUserCredentialModel } from '../api/models/users/user-credential.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'abs-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
  imports: [FormsModule],
})
export class LoginComponent {
  username = '';
  password = '';

  constructor(private store: Store) {}

  login($event: Event) {
    this.store.dispatch(
      new Login({
        emailAddress: this.username,
        password: this.password,
      } as IUserCredentialModel)
    );
  }
}
