import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { UserAccountService } from '../api/fake/user-account.service';
import { IUserAccountModel } from '../api/models/user-account.model';
import { Observable } from 'rxjs';
import { Select, Store } from '@ngxs/store';
import { MyAccountState } from '../state/my-account/my-account.state';
import { GetUser } from '../state/my-account/my-account.actions';
import { FormsModule } from '@angular/forms';
import { NgIf, AsyncPipe } from '@angular/common';

@Component({
  selector: 'abs-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  standalone: true,
  imports: [NgIf, FormsModule, AsyncPipe],
})
export class MyAccountComponent {
  @Select(MyAccountState.user) user$: Observable<IUserAccountModel> | undefined;
  userAccount$: Observable<IUserAccountModel> | undefined;
  isEdit = false;
  profilePicture =
    'https://fastly.picsum.photos/id/91/3504/2336.jpg?hmac=tK6z7RReLgUlCuf4flDKeg57o6CUAbgklgLsGL0UowU';
  constructor(
    private userAccountService: UserAccountService,
    private store: Store
  ) {
    this.getUserAccount();
  }

  getUserAccount() {
    this.store.dispatch(new GetUser());
  }

  // setUserAccount() {}
}
