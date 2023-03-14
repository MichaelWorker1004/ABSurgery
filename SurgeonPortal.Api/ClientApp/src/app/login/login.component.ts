import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import '../../web-components/card-user';
import { User } from '../../web-components/user';

@Component({
  selector: 'abs-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
})
export class LoginComponent {
  user: User = {
    id: 2,
    fullName: 'Home Simpson',
    role: 'Software Engineer',
    avatar: 'assets/img/homer-simpson.png',
  };

  edit($event: Event) {
    console.log('event', $event);
  }
}
