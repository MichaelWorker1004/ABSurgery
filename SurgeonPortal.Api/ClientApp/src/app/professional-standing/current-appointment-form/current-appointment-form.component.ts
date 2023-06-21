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
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable } from 'rxjs';

import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';

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
  ],
  templateUrl: './current-appointment-form.component.html',
  styleUrls: ['./current-appointment-form.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class CurrentAppointmentFormComponent implements OnInit {
  @Output() cancelForm: EventEmitter<any> = new EventEmitter();
  @Output() saveForm: EventEmitter<any> = new EventEmitter();
  @Input() formData: any;
  @Input() isEdit = false;
  @Input() picklists: any;
  @Input() errors$?: Observable<any> | undefined;
  @Input() clearErrors?: any;

  optionLists: OptionList = {
    primaryPracticeOptions: [],

    organizationTypeOptions: [],
  };

  originalFormValues: any;
  localEdit = false;

  currentAppointmentForm = new FormGroup({
    primaryPracticeId: new FormControl({ value: null, disabled: false }, [
      Validators.required,
    ]),

    organizationTypeId: new FormControl({ value: null, disabled: false }, [
      Validators.required,
    ]),

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

  setFormValues(data: any) {
    if (data) {
      for (const [key, value] of Object.entries(data)) {
        let newValue = value;
        if (key.includes('Date')) {
          newValue = new Date(value as any).toLocaleDateString();
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
