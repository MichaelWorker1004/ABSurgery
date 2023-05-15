import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from '../grid/grid.component';
import { PAY_FEE_COLS } from './pay-fee-cols';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { GetStateList, PicklistsSelectors } from 'src/app/state/picklists';
import { Observable } from 'rxjs';
import { IStateReadOnlyModel } from 'src/app/api';
import { Select, Store } from '@ngxs/store';
import { IFormFields } from '../../models/form-fields/form-fields';

@Component({
  selector: 'abs-pay-fee',
  standalone: true,
  imports: [
    CommonModule,
    GridComponent,
    FormsModule,
    InputTextModule,
    DropdownModule,
  ],
  templateUrl: './pay-fee.component.html',
  styleUrls: ['./pay-fee.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class PayFeeComponent implements OnInit {
  @Select(PicklistsSelectors.slices.states) states$:
    | Observable<IStateReadOnlyModel[]>
    | undefined;

  @Output() cancelAction: EventEmitter<any> = new EventEmitter();

  @Input() payFeeData: any;
  @Input() paymentGridData: any;

  states: IStateReadOnlyModel[] = [];

  paymentInformationFormFields: IFormFields[] = [
    {
      label: 'First Name',
      value: '',
      required: true,
      name: 'firstName',
      placeholder: 'Enter your first name',
      type: 'text',
      size: 'col-6',
    },
    {
      label: 'Last Name',
      value: '',
      required: true,
      name: 'lastName',
      placeholder: 'Enter your last name',
      type: 'text',
      size: 'col-6',
    },
    {
      label: 'Email Address',
      value: '',
      required: true,
      name: 'emailAddress',
      placeholder: 'Enter your email address',
      type: 'text',
      size: 'col-6',
    },
    {
      label: 'Phone Number',
      value: '',
      required: true,
      name: 'phoneNumber',
      placeholder: '_ _ _ - _ _ _ - _ _ _ _',
      type: 'text',
      size: 'col-6',
    },
    {
      label: 'Street Address Line 1',
      subLabel: '',
      value: '',
      required: true,
      name: 'streetAddressLine1',
      placeholder: 'Enter your full address',
      type: 'text',
      size: 'col-6',
    },
    {
      label: 'Suite/Floor/Apt',
      subLabel: '',
      value: '',
      required: false,
      name: 'streetAddressLine2',
      placeholder: 'ex. Suite 3',
      type: 'text',
      size: 'col-6',
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
      options: [],
    },
    {
      label: 'Zipcode',
      subLabel: '',
      value: '',
      required: true,
      name: 'zipcode',
      placeholder: 'Enter your zip code',
      type: 'text',
      size: 'col-4',
    },
  ];

  payFeeCols = PAY_FEE_COLS;

  constructor(private _store: Store) {
    this._store.dispatch(new GetStateList('500'));
  }

  ngOnInit(): void {
    this.setPicklists();
  }

  setPicklists() {
    this.states$?.subscribe((states) => {
      this.states = states;
      this.paymentInformationFormFields.filter((fields) => {
        if (fields.name === 'state') {
          fields.options = states;
        }
      });
    });
  }

  handleGridAction(event: any) {
    console.log(event);
  }

  handleCancelAction() {
    this.cancelAction.emit();
  }

  handleSubmitAction() {
    console.log('Submit');
  }
}