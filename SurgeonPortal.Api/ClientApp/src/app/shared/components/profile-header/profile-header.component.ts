import { Component, CUSTOM_ELEMENTS_SCHEMA, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IUserProfile, UserProfileSelectors } from '../../../state';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';

@Component({
  selector: 'abs-profile-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile-header.component.html',
  styleUrls: ['./profile-header.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ProfileHeaderComponent {
  @Input() profilePicture: string | undefined;
  // TODO: [Joe] this will need to be more strongly typed once the model is defined

  user: IUserProfile | undefined;

  constructor(private _store: Store) {
    this.user = this._store.selectSnapshot(UserProfileSelectors.user);
  }
}
