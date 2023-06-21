import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  Output,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';

import { CalendarModule } from 'primeng/calendar';
import { Observable } from 'rxjs';

@Component({
  selector: 'abs-license-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    InputTextModule,
    DropdownModule,
    CalendarModule,
  ],
  templateUrl: './license-form.component.html',
  styleUrls: ['./license-form.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class LicenseFormComponent implements OnInit {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  @Output() saveDialog: EventEmitter<any> = new EventEmitter();
  @Input() formData: any;
  @Input() isEdit = false;
  @Input() picklists: any;
  @Input() errors$?: Observable<any> | undefined;
  @Input() clearErrors?: any;

  optionLists = {
    licenseStateOptions: [],
    licenseTypeOptions: [],
  };

  originalFormValues: any;
  localEdit = false;

  medicalLicenseForm = new FormGroup({
    issuingStateId: new FormControl({ value: null, disabled: false }, [
      Validators.required,
    ]),

    licenseNumber: new FormControl({ value: '', disabled: false }, [
      Validators.required,
    ]),

    licenseTypeId: new FormControl({ value: null, disabled: false }, [
      Validators.required,
    ]),

    issueDate: new FormControl({ value: '', disabled: false }, [
      Validators.required,
    ]),

    expireDate: new FormControl({ value: '', disabled: false }, [
      Validators.required,
    ]),
  });

  ngOnInit() {
    this.optionLists = { ...this.optionLists, ...this.picklists };

    this.originalFormValues = this.formData;
    this.setFormValues(this.originalFormValues);
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['formData']) {
      this.originalFormValues = changes['formData'].currentValue;
      this.setFormValues(this.originalFormValues);
    }
    if (changes['isEdit']) {
      this.localEdit = changes['isEdit'].currentValue;
    }
    if (changes['picklists']) {
      this.optionLists = {
        ...this.optionLists,
        ...changes['picklists'].currentValue,
      };
    }
  }

  setFormValues(data: any) {
    if (data) {
      for (const [key, value] of Object.entries(data)) {
        let newValue = value;
        if (key.includes('Date')) {
          newValue = new Date(value as any).toLocaleDateString();
        }
        this.medicalLicenseForm.get(key)?.setValue(newValue);
      }
    } else {
      this.medicalLicenseForm.reset();
    }
  }

  onSubmit() {
    this.saveDialog.emit({
      show: false,
      data: this.medicalLicenseForm.getRawValue(),
      isEdit: this.localEdit,
    });
  }

  close() {
    this.closeDialog.emit({ show: false });
  }
}
