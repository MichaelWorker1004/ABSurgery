import { CUSTOM_ELEMENTS_SCHEMA, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'abs-alert',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AlertComponent {
  /**
   * the type of alert to dispaly
   */
  @Input() alertType!: 'success' | 'info' | 'warning' | 'danger';

  /**
   * boolean flag to hide or display the alert icon
   */
  @Input() hideIcon = false;

  /**
   * font size used for the alert content
   */
  @Input() fontSize = 1.25;
}
