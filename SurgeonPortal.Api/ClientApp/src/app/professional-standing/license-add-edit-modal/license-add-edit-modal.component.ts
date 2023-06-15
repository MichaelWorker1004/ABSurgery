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
import { CalendarModule } from 'primeng/calendar';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'abs-license-add-edit-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    InputTextModule,
    DropdownModule,
    CalendarModule,
    ButtonModule,
  ],
  templateUrl: './license-add-edit-modal.component.html',
  styleUrls: ['./license-add-edit-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class LicenseAddEditModalComponent implements OnInit {
  @Input() license: Subject<any> = new Subject();
  @Output() cancelDialog: EventEmitter<any> = new EventEmitter();
  @Output() saveDialog: EventEmitter<any> = new EventEmitter();

  fakeOptions = [
    { itemDescription: 'Option 1', itemValue: 'option-1' },
    { itemDescription: 'Option 2', itemValue: 'option-2' },
    { itemDescription: 'Option 3', itemValue: 'option-3' },
  ];

  localLicense: any = {};

  ngOnInit() {
    this.license.subscribe((value) => {
      this.localLicense = value;
      if (value.issueDate) {
        // TODO: [Joe] convert to string format yyyy-MM-dd
        // this.localLicense.issueDate = new Date(value.issueDate)
        //   .toISOString()
        //   .split('T')[0];
      }
      if (value.expireDate) {
        // this.localLicense.expireDate = new Date(value.expireDate)
        //   .toISOString()
        //   .split('T')[0];
      }
    });
  }

  handleDefaultClose(event: any) {
    event.preventDefault();
  }

  cancel() {
    this.cancelDialog.emit({ show: false });
  }

  save() {
    this.saveDialog.emit({ show: false, license: this.localLicense });
  }
}
