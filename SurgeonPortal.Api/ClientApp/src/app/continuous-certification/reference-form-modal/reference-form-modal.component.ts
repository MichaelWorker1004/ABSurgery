import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import { FormsModule } from '@angular/forms';
import { REFERENCE_FORMS_COLS } from './refrence-forms-cols';

@Component({
  selector: 'abs-reference-form-modal',
  standalone: true,
  imports: [CommonModule, GridComponent, FormsModule],
  templateUrl: './reference-form-modal.component.html',
  styleUrls: ['./reference-form-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ReferenceFormModalComponent {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  userData = {
    diplayName: 'John Doe, M.D',
  };

  referenceFormsCols = REFERENCE_FORMS_COLS;
  referenceFormFields = [
    {
      label: 'Name of Authenticating Official',
      subLabel: '(Must be a Physician)',
      value: '',
      required: true,
      name: 'nameOfAuthenticatingOfficial',
      placeholder: 'Enter Official’s name',
      type: 'text',
      size: 'col-4',
    },
    {
      label: "Authenticating Official'/s Role",
      subLabel: '',
      value: '',
      required: true,
      name: 'authenticatingOfficialRole',
      placeholder: 'Choose their role',
      type: 'select',
      size: 'col-4',
      options: [
        {
          label: 'Chief of Staff',
          value: 'chief',
        },
        {
          label: 'Medical Director',
          value: 'medical',
        },
        {
          label: 'Program Director',
          value: 'program',
        },
      ],
    },
    {
      label: 'Reason for Alternate Official',
      subLabel: '',
      value: '',
      required: false,
      name: 'reasonForAlternateOfficial',
      placeholder: 'Enter a reason',
      type: 'select',
      size: 'col-4',
      options: [
        {
          label: 'Option 1',
          value: 'option1',
        },
        {
          label: 'Option 2',
          value: 'option2',
        },
        {
          label: 'Option 3',
          value: 'option3',
        },
      ],
    },
    {
      label: 'Authenticating Official’s Title',
      subLabel: '',
      value: '',
      required: false,
      name: 'authenticatingOfficialTitle',
      placeholder: 'Enter Official’s title',
      type: 'text',
      size: 'col-12',
    },
    {
      label: 'Authenticating Official’s Email Address',
      subLabel: '',
      value: '',
      required: true,
      name: 'authenticatingOfficialEmail',
      placeholder: 'Enter Official’s email address',
      type: 'text',
      size: 'col-4',
    },
    {
      label: 'Confirm Email Address',
      subLabel: '',
      value: '',
      required: true,
      name: 'confirmEmailAddress',
      placeholder: 'Enter Official’s email address again',
      type: 'text',
      size: 'col-4',
    },
    {
      label: 'Authenticating Official’s Phone Number',
      subLabel: '',
      value: '',
      required: true,
      name: 'authenticatingOfficialPhoneNumber',
      placeholder: '_ _ _ - _ _ _ - _ _ _ _',
      type: 'text',
      size: 'col-4',
    },
    {
      label: 'Name of Affiliated Institution',
      subLabel: '',
      value: '',
      required: false,
      name: 'nameOfAffiliatedInstitution',
      placeholder: 'Enter affiliated institution',
      type: 'text',
      size: 'col-12',
    },
    {
      label: 'City',
      subLabel: '',
      value: '',
      required: true,
      name: 'city',
      placeholder: 'Enter your city',
      type: 'text',
      size: 'col-4',
    },
    {
      label: 'State',
      subLabel: '',
      value: '',
      required: true,
      name: 'state',
      placeholder: 'Choose your state',
      type: 'select',
      size: 'col-4',
      options: [
        {
          label: 'Alabama',
          value: 'AL',
        },
        {
          label: 'Alaska',
          value: 'AK',
        },
      ],
    },
    {
      label: 'Name',
      subLabel: '',
      value: this.userData.diplayName,
      required: false,
      readonly: true,
      name: 'name',
      placeholder: 'Enter your full name',
      type: 'text',
      size: 'col-12',
    },
  ];
  refrenceGridData = [
    {
      referenceFormId: 'MD19143',
      affiliatedInstitution: 'ABS',
      authenticatingOfficial: 'John Doe, M.D.',
      date: new Date('09/21/2019'),
      status: 'Requested',
    },
    {
      referenceFormId: 'MD08221',
      affiliatedInstitution: 'ABS',
      authenticatingOfficial: 'Mary Joseph',
      date: new Date('08/12/2019'),
      status: 'Approved',
    },
    {
      referenceFormId: 'MD12345',
      affiliatedInstitution: 'ABS',
      authenticatingOfficial: 'John Dorian',
      date: new Date('8/1/2019'),
      status: 'Approved',
    },
  ];

  handleGridAction(event: any) {
    console.log(event);
  }

  onSubmit(formFields: any) {
    console.log(formFields);
  }

  close() {
    this.closeDialog.emit();
  }
}
