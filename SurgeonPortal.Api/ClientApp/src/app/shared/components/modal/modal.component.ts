import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
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
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  @Input() open = false;
  @Input() title: string | undefined | null;
  @Input() status: 'completed' | 'in-progress' | 'alert' | undefined;
  @Input() modalName!: any;
  @Input() preventOverlayClose = false;
  @Input() hideClose = false;
  @Input() width = 70;

  close(event: any) {
    if (this.preventOverlayClose && event.detail.source === 'overlay') {
      event.preventDefault();
    } else {
      this.closeDialog.emit({ action: this.modalName });
    }
  }
}
