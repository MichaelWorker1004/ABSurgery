import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  Output,
  OnInit,
  SimpleChanges,
  OnChanges,
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

import { AutoCompleteModule } from 'primeng/autocomplete';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable } from 'rxjs';

interface OptionList {
  practiceTypeOptions: any[];
  appointmentTypeOptions: any[];
  organizationTypeOptions: any[];
  stateCodeOptions: any[];
  organizationOptions: any[];
  filteredOrganizationOptions: any[];
}

@UntilDestroy()
@Component({
  selector: 'abs-appointments-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    InputTextModule,
    DropdownModule,

    AutoCompleteModule,
  ],
  templateUrl: './appointments-form.component.html',
  styleUrls: ['./appointments-form.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppointmentsFormComponent implements OnInit, OnChanges {
  @Output() cancelForm: EventEmitter<any> = new EventEmitter();
  @Output() saveForm: EventEmitter<any> = new EventEmitter();
  @Input() formData: any;
  @Input() isEdit = false;
  @Input() picklists: any;
  @Input() errors$?: Observable<any> | undefined;
  @Input() clearErrors?: any;

  optionLists: OptionList = {
    practiceTypeOptions: [],

    appointmentTypeOptions: [],

    organizationTypeOptions: [],

    stateCodeOptions: [],

    organizationOptions: [],

    filteredOrganizationOptions: [],
  };

  originalFormValues: any;
  localEdit = false;

  hospitalAppointmentForm = new FormGroup({
    practiceTypeId: new FormControl({ value: null, disabled: false }, [
      Validators.required,
    ]),

    appointmentTypeId: new FormControl({ value: null, disabled: false }, [
      Validators.required,
    ]),

    organizationTypeId: new FormControl({ value: null, disabled: false }, [
      Validators.required,
    ]),

    stateCode: new FormControl({ value: null, disabled: false }, [
      Validators.required,
    ]),

    organizationId: new FormControl({ value: '', disabled: false }, [
      Validators.required,
    ]),

    other: new FormControl({ value: '', disabled: false }),

    authorizingOfficial: new FormControl({ value: '', disabled: false }, [
      Validators.required,
    ]),
  });

  ngOnInit() {
    this.optionLists = { ...this.optionLists, ...this.picklists };

    this.originalFormValues = this.formData;
    this.setFormValues(this.originalFormValues);
    this.onFormChanges();
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

  onFormChanges() {
    this.hospitalAppointmentForm
      .get('organizationId')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        if (val) {
          this.hospitalAppointmentForm
            .get('other')
            ?.setValue('', { emitEvent: false });
          this.hospitalAppointmentForm.get('other')?.disable();
          this.hospitalAppointmentForm.get('other')?.clearValidators();
        } else {
          this.hospitalAppointmentForm.get('other')?.enable();
          this.hospitalAppointmentForm
            .get('other')
            ?.setValidators([Validators.required]);
        }
      });
    this.hospitalAppointmentForm
      .get('other')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        if (val) {
          this.hospitalAppointmentForm.get('organizationId')?.clearValidators();
          this.hospitalAppointmentForm
            .get('organizationId')
            ?.updateValueAndValidity();
        } else {
          this.hospitalAppointmentForm
            .get('organizationId')
            ?.setValidators([Validators.required]);
        }
      });
  }

  setFormValues(data: any) {
    if (data) {
      for (const [key, value] of Object.entries(data)) {
        let newValue = value;
        if (key.includes('Date')) {
          newValue = new Date(value as any).toLocaleDateString();
        }

        if (key === 'organizationId') {
          newValue = this.optionLists.organizationOptions.find(
            (i) => i.itemValue === value
          );
        }
        this.hospitalAppointmentForm.get(key)?.setValue(newValue);
      }
    } else {
      this.hospitalAppointmentForm.reset();
    }
  }

  filterItems($event: any, options: string, filteredOptions: string) {
    const value = $event.query;
    this.optionLists[filteredOptions as keyof OptionList] = this.optionLists[
      options as keyof OptionList
    ]?.filter((i) => {
      return i.itemDescription.toLowerCase().includes(value.toLowerCase());
    });
  }

  onSubmit() {
    this.saveForm.emit({
      show: false,
      data: this.hospitalAppointmentForm.getRawValue(),
      isEdit: this.localEdit,
    });
  }

  close() {
    this.cancelForm.emit({ show: false });
  }
}
