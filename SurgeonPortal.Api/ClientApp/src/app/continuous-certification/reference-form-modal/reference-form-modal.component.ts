import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import { FormsModule } from '@angular/forms';
import { REFERENCE_FORMS_COLS } from './refrence-forms-cols';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { IFormFields } from 'src/app/shared/models/form-fields/form-fields';
import { Select } from '@ngxs/store';
import { PicklistsSelectors } from 'src/app/state/picklists';
import { Observable } from 'rxjs';
import { IStateReadOnlyModel } from 'src/app/api';
import { ButtonModule } from 'primeng/button';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'abs-reference-form-modal',
  standalone: true,
  imports: [
    CommonModule,
    GridComponent,
    FormsModule,
    InputTextModule,
    DropdownModule,
    CheckboxModule,
    ButtonModule,
  ],
  templateUrl: './reference-form-modal.component.html',
  styleUrls: ['./reference-form-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ReferenceFormModalComponent implements OnInit {
  @Select(PicklistsSelectors.slices.states) states$:
    | Observable<IStateReadOnlyModel[]>
    | undefined;

  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  userData = {
    diplayName: 'John Doe, M.D',
  };

  referenceFormsCols = REFERENCE_FORMS_COLS;
  referenceFormFields: IFormFields[] = [
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
          itemDescription: 'Chief of Staff',
          itemValue: 'chief',
        },
        {
          itemDescription: 'Medical Director',
          itemValue: 'medical',
        },
        {
          itemDescription: 'Program Director',
          itemValue: 'program',
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
          itemDescription: 'Option 1',
          itemValue: 'option1',
        },
        {
          itemDescription: 'Option 2',
          itemValue: 'option2',
        },
        {
          itemDescription: 'Option 3',
          itemValue: 'option3',
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
      name: 'states',
      placeholder: 'Choose your state',
      type: 'select',
      size: 'col-4',
      options: [],
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

  ngOnInit(): void {
    this.setPicklists();
  }

  setPicklists() {
    this.states$?.pipe(untilDestroyed(this)).subscribe((states) => {
      this.referenceFormFields.filter((field) => {
        if (field.name === 'states') {
          field.options = states;
        }
      });
    });
  }

  handleGridAction(event: any) {
    console.log('unhandled action', event);
  }

  onSubmit(formFields: any) {
    console.log('unhandled submit', formFields);
  }

  close() {
    this.closeDialog.emit();
  }
}
