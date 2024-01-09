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
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable } from 'rxjs';

import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FormErrorsComponent } from 'src/app/shared/components/form-errors/form-errors.component';
import { CheckboxModule } from 'primeng/checkbox';

// add error handling to this schematic

interface OptionList {
  primaryPracticeOptions: any[];

  organizationTypeOptions: any[];
}

@UntilDestroy()
@Component({
  selector: 'abs-current-appointment-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    DropdownModule,
    InputTextareaModule,
    FormErrorsComponent,
    CheckboxModule,
  ],
  templateUrl: './current-appointment-form.component.html',
  styleUrls: ['./current-appointment-form.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class CurrentAppointmentFormComponent implements OnInit, OnChanges {
  @Output() cancelForm: EventEmitter<any> = new EventEmitter();
  @Output() saveForm: EventEmitter<any> = new EventEmitter();
  @Input() formData: any;
  @Input() isEdit = false;
  @Input() picklists: any;
  @Input() errors$?: Observable<any> | undefined;
  @Input() clearErrors?: any;

  optionLists: OptionList = {
    primaryPracticeOptions: [] as any[],

    organizationTypeOptions: [] as any[],
  };

  originalFormValues: any;
  localEdit = false;

  currentAppointmentForm = new FormGroup({
    clinicallyActive: new FormControl({ value: false, disabled: true }),
    primaryPracticeId: new FormControl<number | null>(
      { value: null, disabled: true },
      [Validators.required]
    ),

    organizationTypeId: new FormControl<number | null>(
      { value: null, disabled: true },
      [Validators.required]
    ),

    explanationOfNonPrivileges: new FormControl({ value: '', disabled: false }),

    explanationOfNonClinicalActivities: new FormControl({
      value: '',
      disabled: false,
    }),
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

  onClinicalActiveChange(event: any) {
    const checked = event.checked;

    if (checked) {
      this.currentAppointmentForm.get('primaryPracticeId')?.enable();
      this.currentAppointmentForm
        .get('primaryPracticeId')
        ?.setValidators([Validators.required]);
      this.currentAppointmentForm.get('organizationTypeId')?.enable();
      this.currentAppointmentForm
        .get('organizationTypeId')
        ?.setValidators([Validators.required]);
      this.currentAppointmentForm.get('explanationOfNonPrivileges')?.disable();
      this.currentAppointmentForm
        .get('explanationOfNonPrivileges')
        ?.clearValidators();
      this.currentAppointmentForm
        .get('explanationOfNonPrivileges')
        ?.setValue(null);
      this.currentAppointmentForm
        .get('explanationOfNonClinicalActivities')
        ?.disable();
      this.currentAppointmentForm
        .get('explanationOfNonClinicalActivities')
        ?.setValue('');
    } else {
      this.currentAppointmentForm.get('primaryPracticeId')?.disable();
      this.currentAppointmentForm.get('primaryPracticeId')?.clearValidators();
      this.currentAppointmentForm.get('primaryPracticeId')?.setValue(null);
      this.currentAppointmentForm.get('organizationTypeId')?.disable();
      this.currentAppointmentForm.get('organizationTypeId')?.clearValidators();
      this.currentAppointmentForm.get('organizationTypeId')?.setValue(null);
      this.currentAppointmentForm.get('explanationOfNonPrivileges')?.enable();
      this.currentAppointmentForm
        .get('explanationOfNonPrivileges')
        ?.setValidators([Validators.required]);
      this.currentAppointmentForm
        .get('explanationOfNonClinicalActivities')
        ?.enable();
    }
  }

  setFormValues(data: any) {
    if (data) {
      for (const [key, value] of Object.entries(data)) {
        let newValue = value;
        if (key.includes('Date')) {
          newValue = new Date(value as any).toLocaleDateString();
        }

        if (key === 'clinicallyActive') {
          newValue = value === 1 ? true : false;
          this.onClinicalActiveChange({ checked: newValue });
        }

        this.currentAppointmentForm.get(key)?.setValue(newValue);
      }
    } else {
      this.currentAppointmentForm.reset();
    }
  }

  onFormChanges() {
    // include subscriptions to .valueChanges here for the reactive form
    // be sure to include .pipe(untilDestroyed(this)) to the subscriptions
    this.currentAppointmentForm
      .get('clinicallyActive')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        this.onClinicalActiveChange({ checked: val });
      });
  }

  onSubmit() {
    this.saveForm.emit({
      show: false,
      data: this.currentAppointmentForm.getRawValue(),
      isEdit: this.localEdit,
    });
  }

  close() {
    this.cancelForm.emit({ show: false });
  }
}
