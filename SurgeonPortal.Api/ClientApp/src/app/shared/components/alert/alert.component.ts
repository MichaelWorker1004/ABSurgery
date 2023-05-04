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
  @Input() alertType!: string;
  @Input() hideIcon = false;
  @Input() fontSize = 1.25;
}
