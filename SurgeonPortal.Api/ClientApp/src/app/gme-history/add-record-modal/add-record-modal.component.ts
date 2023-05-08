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

@Component({
  selector: 'abs-add-record-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InputSelectComponent,
  ],
  templateUrl: './add-record-modal.component.html',
  styleUrls: ['./add-record-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AddRecordModalComponent implements OnInit {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  @Input() isEdit$ = new BehaviorSubject<boolean>(false);
  @Input() slectedGmeRotation$ = new BehaviorSubject<any[]>([]);

  addEditRecordFields = [
    {
      label: 'Start Date',
      subLabel: '',
      value: '',
      required: true,
      name: 'from',
      placeholder: 'MM/DD/YYYY',
      type: 'date',
      size: 'col-4',
    },
    {
      label: 'End Date',
      subLabel: '',
      required: true,
      name: 'to',
      placeholder: 'MM/DD/YYYY',
      type: 'date',
      size: 'col-4',
    },
    {
      label: 'Week(s)',
      required: false,
      name: 'weeks',
      placeholder: '',
      readonly: true,
      type: 'text',
      size: 'col-4',
    },
    {
      label: 'Program Name',
      subLabel: '',
      required: true,
      name: 'programName',
      placeholder: 'Type your answer...',
      type: 'text',
      size: 'col-6',
    },
    {
      label: 'Affiliated Organization',
      subLabel: '',
      required: true,
      name: 'affiliatedOrganization',
      placeholder: 'Type your answer...',
      type: 'text',
      size: 'col-6',
    },
    {
      label: 'Clinical Level',
      subLabel: '',
      required: true,
      name: 'clinicalLevel',
      placeholder: 'Type your answer...',
      type: 'text',
      size: 'col-12',
    },
    {
      label: 'Explain',
      subLabel: '',
      required: true,
      name: 'explain',
      placeholder: 'Type your answer...',
      type: 'textarea',
      size: 'col-6',
    },
    {
      label: 'Description (Non-Surgical Only)',
      subLabel: '',
      required: true,
      name: 'description',
      placeholder: 'Type your answer...',
      type: 'textarea',
      size: 'col-6',
    },
    {
      label: 'International Rotation',
      subLabel: '',
      value: '',
      required: true,
      name: 'internationalRotation',
      placeholder: 'Make a selection...',
      type: 'select',
      size: 'col-4',
      options: [
        {
          label: 'Yes',
          value: 'Yes',
        },
        {
          label: 'No',
          value: 'No',
        },
      ],
    },
  ];

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
