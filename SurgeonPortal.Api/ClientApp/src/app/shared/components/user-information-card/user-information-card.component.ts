import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'abs-user-information-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-information-card.component.html',
  styleUrls: ['./user-information-card.component.scss'],
})
export class UserInformationCardComponent {
  @Input() userInformation: any;
  @Input() currentStatus: any;
}
