import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'abs-success-fail-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './success-fail-modal.component.html',
  styleUrls: ['./success-fail-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class SuccessFailModalComponent {
  @Input() isSuccess!: boolean;
  @Input() message!: string;
  @Input() title!: string;
  @Input() showDialog = false;
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  close() {
    console.log('modal close');
    this.closeDialog.emit({ show: false });
  }
}
