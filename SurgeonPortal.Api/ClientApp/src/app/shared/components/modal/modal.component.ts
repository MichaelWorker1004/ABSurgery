import { CUSTOM_ELEMENTS_SCHEMA, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'abs-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ModalComponent {
  @Input() open = false;
  @Input() title!: string | undefined;
  @Input() status!: string | undefined;
  @Input() hideClose = false;
}
