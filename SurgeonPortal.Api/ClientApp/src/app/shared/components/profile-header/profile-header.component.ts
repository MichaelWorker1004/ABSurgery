import { Component, CUSTOM_ELEMENTS_SCHEMA, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IUserProfile, UserProfileSelectors } from '../../../state';
import { Select } from '@ngxs/store';
import { Observable } from 'rxjs';
import { UserClaims } from 'src/app/side-navigation/user-status.enum';

@Component({
  selector: 'abs-profile-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile-header.component.html',
  styleUrls: ['./profile-header.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ProfileHeaderComponent {
  /**
   * Loads user profile from the state store
   * @type {Observable<IUserProfile>}
   */
  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  /**
   *
   * Profile picture to display in the header
   * @type {string}
   */
  @Input() profilePicture: string | undefined;

  /**
   * User claims enum to display in the header
   */
  userClaims = UserClaims;

  // TODO: [Joe] we need to get status from something other than the second item in the list of user claims
}
