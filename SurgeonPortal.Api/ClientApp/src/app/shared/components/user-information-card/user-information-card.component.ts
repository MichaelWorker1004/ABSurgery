import { CommonModule } from '@angular/common';
import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'abs-user-information-card',
  standalone: true,
  imports: [CommonModule, TranslateModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './user-information-card.component.html',
  styleUrls: ['./user-information-card.component.scss'],
})
export class UserInformationCardComponent implements OnInit, OnChanges {
  /**
   *  User information to display. See Examples below
   */
  @Input() userInformation: any;

  /**
   * Current status of the user
   * @type {string}
   */
  @Input() currentStatus!: string;

  /**
   * Whether the user is a surgeon or not
   * @type {boolean}
   */
  @Input() isSurgeon: boolean | undefined;

  /**
   * Meeting requiremnts status for the user
   * @type {string}
   */
  @Input() meetingRequirements: string | null | undefined;

  @Input() userId: number | undefined;

  localIsSurgeon!: boolean;
  localCurrentStatus!: string;

  ngOnInit(): void {
    this.localIsSurgeon = this.isSurgeon || false;
    this.localCurrentStatus = this.currentStatus || 'Not Certified';
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['isSurgeon']) {
      this.localIsSurgeon = changes['isSurgeon'].currentValue;
    }
    if (changes['currentStatus']) {
      this.localCurrentStatus = changes['currentStatus'].currentValue;
    }
  }

  navigateToCredentialSearch() {
    window.open(`${environment.credentialSearch}?userId=${this.userId}`);
  }
}
