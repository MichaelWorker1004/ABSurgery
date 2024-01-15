import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  CUSTOM_ELEMENTS_SCHEMA,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';

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
}
