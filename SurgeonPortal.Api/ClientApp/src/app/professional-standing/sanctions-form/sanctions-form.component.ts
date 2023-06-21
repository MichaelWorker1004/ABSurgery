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
import { InputTextareaModule } from 'primeng/inputtextarea';
import { RadioButtonModule } from 'primeng/radiobutton';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable } from 'rxjs';

@UntilDestroy()
@Component({
  selector: 'abs-sanctions-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    InputTextareaModule,
    RadioButtonModule,
  ],
  templateUrl: './sanctions-form.component.html',
  styleUrls: ['./sanctions-form.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class SanctionsFormComponent implements OnInit {
  @Output() cancelForm: EventEmitter<any> = new EventEmitter();
  @Output() saveForm: EventEmitter<any> = new EventEmitter();
  @Input() formData: any;
  @Input() isEdit = false;
  @Input() picklists: any;
  @Input() errors$?: Observable<any> | undefined;
  @Input() clearErrors?: any;

  optionLists = {
    yesNoOptions: [
      { label: 'Yes', value: true },
      { label: 'No', value: false },
    ],
  };

  originalFormValues: any;
  localEdit = false;

  sanctionsEthicsForm = new FormGroup({
    hadDrugAlchoholTreatment: new FormControl(
      { value: null, disabled: false },
      [Validators.required]
    ),

    hadHospitalPrivilegesDenied: new FormControl(
      { value: null, disabled: false },
      [Validators.required]
    ),

    hadLicenseRestricted: new FormControl({ value: null, disabled: false }, [
      Validators.required,
    ]),

    hadHospitalPrivilegesRestricted: new FormControl(
      { value: null, disabled: false },
      [Validators.required]
    ),

    hadFelonyConviction: new FormControl({ value: null, disabled: false }, [
      Validators.required,
    ]),

    hadCensure: new FormControl({ value: null, disabled: false }, [
      Validators.required,
    ]),

    explanation: new FormControl({ value: null, disabled: false }, [
      Validators.required,
    ]),
  });

  ngOnInit() {
    this.optionLists = { ...this.optionLists, ...this.picklists };

    this.originalFormValues = this.formData;
    this.setFormValues(this.originalFormValues);
    this.onFormChanges();
    this.checkSantionsAndEthics();
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
    this.sanctionsEthicsForm
      .get('hadDrugAlchoholTreatment')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        this.checkSantionsAndEthics();
      });
    this.sanctionsEthicsForm
      .get('hadHospitalPrivilegesDenied')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        this.checkSantionsAndEthics();
      });
    this.sanctionsEthicsForm
      .get('hadLicenseRestricted')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        this.checkSantionsAndEthics();
      });
    this.sanctionsEthicsForm
      .get('hadHospitalPrivilegesRestricted')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        this.checkSantionsAndEthics();
      });
    this.sanctionsEthicsForm
      .get('hadFelonyConviction')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        this.checkSantionsAndEthics();
      });
    this.sanctionsEthicsForm
      .get('hadCensure')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        this.checkSantionsAndEthics();
      });
  }

  setFormValues(data: any) {
    if (data) {
      for (const [key, value] of Object.entries(data)) {
        let newValue = value;
        if (key.includes('Date')) {
          newValue = new Date(value as any).toLocaleDateString();
        }
        this.sanctionsEthicsForm.get(key)?.setValue(newValue);
      }
    } else {
      this.sanctionsEthicsForm.reset();
    }
  }

  checkSantionsAndEthics() {
    const disableDescribe = !Object.values(
      this.sanctionsEthicsForm.getRawValue()
    ).some((value) => value === true);
    if (disableDescribe) {
      this.sanctionsEthicsForm.get('explanation')?.disable();
      this.sanctionsEthicsForm.get('explanation')?.patchValue(null);
      this.sanctionsEthicsForm.get('explanation')?.clearValidators();
    } else {
      this.sanctionsEthicsForm.get('explanation')?.enable();
      this.sanctionsEthicsForm
        .get('explanation')
        ?.setValidators([Validators.required]);
    }
    this.sanctionsEthicsForm.get('explanation')?.updateValueAndValidity();
  }

  onSubmit() {
    this.saveForm.emit({
      show: false,
      data: this.sanctionsEthicsForm.getRawValue(),
      isEdit: this.localEdit,
    });
  }

  close() {
    this.cancelForm.emit({ show: false });
  }
}
