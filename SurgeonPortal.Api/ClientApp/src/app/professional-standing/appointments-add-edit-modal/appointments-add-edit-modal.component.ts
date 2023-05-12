import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Subject } from 'rxjs';

import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';

@Component({
  selector: 'abs-appointments-add-edit-modal',
  standalone: true,
  imports: [CommonModule, FormsModule, InputTextModule, DropdownModule],
  templateUrl: './appointments-add-edit-modal.component.html',
  styleUrls: ['./appointments-add-edit-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppointmentsAddEditModalComponent implements OnInit {
  // TODO: [Joe] this should be strongly typed once models are created
  @Input() appointment: Subject<any> = new Subject();
  @Output() cancelDialog: EventEmitter<any> = new EventEmitter();
  @Output() saveDialog: EventEmitter<any> = new EventEmitter();

  fakeOptions = [
    { itemDescription: 'Option 1', itemValue: 'option-1' },
    { itemDescription: 'Option 2', itemValue: 'option-2' },
    { itemDescription: 'Option 3', itemValue: 'option-3' },
  ];

  localAppointment: any;

  ngOnInit() {
    this.appointment.subscribe((value) => {
      this.localAppointment = value;
    });
  }

  handleDefaultClose(event: any) {
    event.preventDefault();
  }

  cancel() {
    this.cancelDialog.emit({ show: false });
  }

  save() {
    this.saveDialog.emit({ show: false, appointment: this.localAppointment });
  }
}
