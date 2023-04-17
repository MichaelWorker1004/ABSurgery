import { Component, CUSTOM_ELEMENTS_SCHEMA, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IUserProfile, UserProfileSelectors } from '../../../state';
import { Select } from '@ngxs/store';
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
  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;
}
