import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { BehaviorSubject } from 'rxjs';
import { InputSelectComponent } from 'src/app/shared/components/base-input/input-select.component';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CalendarModule } from 'primeng/calendar';
import { IFormFields } from 'src/app/shared/models/form-fields/form-fields';
import { ADD_EDIT_RECORD_FIELDS } from './add-record-form-fields';

@Component({
  selector: 'abs-add-record-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InputSelectComponent,
    InputTextModule,
    DropdownModule,
    InputTextareaModule,
    CalendarModule,
  ],
  templateUrl: './add-record-modal.component.html',
  styleUrls: ['./add-record-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AddRecordModalComponent implements OnInit {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  @Input() isEdit$ = new BehaviorSubject<boolean>(false);
  @Input() slectedGmeRotation$ = new BehaviorSubject<any[]>([]);

  addEditRecordFields: IFormFields[] = ADD_EDIT_RECORD_FIELDS;

  addEditRecordsForm = new FormGroup({
    to: new FormControl('', [Validators.required]),
    from: new FormControl('', [Validators.required]),
    weeks: new FormControl(''),
    programName: new FormControl('', [Validators.required]),
    affiliatedOrganization: new FormControl('', [Validators.required]),
    clinicalLevel: new FormControl('', [Validators.required]),
    explain: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    internationalRotation: new FormControl('', [Validators.required]),
  });

  ngOnInit() {
    this.fetchFormData();
    this.onChanges();
  }

  onChanges() {
    const calculateWeeks = () => {
      const startDate = this.addEditRecordsForm.get('to')?.value
        ? new Date(this.addEditRecordsForm.get('to')?.value as string)
        : undefined;

      const endDate = this.addEditRecordsForm.get('from')?.value
        ? new Date(this.addEditRecordsForm.get('from')?.value as string)
        : undefined;

      if (startDate && endDate) {
        const diffTime = Math.abs(endDate.getTime() - startDate.getTime());
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
        const weeks = diffDays / 7;
        let weeksValue: string | undefined;
        if (weeks >= 1) {
          weeksValue = Math.round(weeks).toString();
          this.addEditRecordFields.filter((field) => {
            if (field.name === 'weeks') {
              field.label = 'Week(s)';
            }
          });
        } else {
          this.addEditRecordFields.filter((field) => {
            if (field.name === 'weeks') {
              field.label = 'Day(s)';
            }
          });
          weeksValue = diffDays.toString();
        }

        this.addEditRecordsForm.get('weeks')?.setValue(weeksValue.toString());
      }
    };

    this.addEditRecordsForm.get('to')?.valueChanges.subscribe(() => {
      calculateWeeks();
    });

    this.addEditRecordsForm.get('from')?.valueChanges.subscribe(() => {
      calculateWeeks();
    });
  }

  fetchFormData() {
    this.isEdit$.subscribe((isEdit) => {
      if (isEdit) {
        this.slectedGmeRotation$.subscribe((formData) => {
          for (const [key, value] of Object.entries(formData)) {
            this.addEditRecordsForm.get(key)?.setValue(value);
          }
        });
      }
    });
  }

  onSubmit() {
    const formValues = this.addEditRecordsForm.getRawValue();
    console.log('POST!', formValues);
  }

  close() {
    this.closeDialog.emit();
  }
}
