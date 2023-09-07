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
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { GetStateList, PicklistsSelectors } from 'src/app/state/picklists';
import { Observable } from 'rxjs';
import { IStateReadOnlyModel } from 'src/app/api';
import { Select, Store } from '@ngxs/store';
import { IFormFields } from '../../models/form-fields/form-fields';
import { ButtonModule } from 'primeng/button';
import { IUserProfile, UserProfileSelectors } from 'src/app/state';
import { PAY_FEE_FORM_FIELDS } from './pay-fee-form-fields';

@Component({
  selector: 'abs-pay-fee',
  standalone: true,
  imports: [
    CommonModule,
    GridComponent,
    FormsModule,
    InputTextModule,
    DropdownModule,
    ButtonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './pay-fee.component.html',
  styleUrls: ['./pay-fee.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class PayFeeComponent implements OnInit {
  @Select(PicklistsSelectors.slices.states) states$:
    | Observable<IStateReadOnlyModel[]>
    | undefined;

  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  @Output() cancelAction: EventEmitter<any> = new EventEmitter();
  @Input() payFeeData: any;
  @Input() paymentGridData: any;

  states: IStateReadOnlyModel[] = [];

  paymentInformationFormFields: IFormFields[] = PAY_FEE_FORM_FIELDS;
  payFeeCols = PAY_FEE_COLS;

  // paymentInformationForm: FormGroup = new FormGroup({
  //   emailAddress: new FormControl('', []), //readonly
  //   state: new FormControl('', []), //sometimes no options
  //   street2: new FormControl('', []), //sometimes no valid value
  //   mobilePhoneNumber: new FormControl('', []), // typically not required in forms
  //   country: new FormControl('', [Validators.required]),
  //   firstName: new FormControl('', [Validators.required]),
  //   lastName: new FormControl('', [Validators.required]),
  //   street1: new FormControl('', [Validators.required]),
  //   zipCode: new FormControl('', [Validators.required]),
  // });

  paymentInformationForm: FormGroup = new FormGroup({});

  constructor(private _store: Store) {
    this._store.dispatch(new GetStateList('500'));
  }

  ngOnInit(): void {
    this.setPicklists();
    this.setUpFormFields();
  }

  async setUpFormFields() {
    const promises = this.paymentInformationFormFields.map((field) => {
      return new Promise<void>((resolve, reject) => {
        this.paymentInformationForm.addControl(
          field.name,
          new FormControl(field.value, field.validators)
        );
        resolve();
      });
    });

    await Promise.all(promises);
    this.populateFormFields();
  }

  populateFormFields() {
    this.user$?.subscribe((user) => {
      for (const [key, value] of Object.entries(user)) {
        this.paymentInformationForm.get(key)?.setValue(value);
      }
    });
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
    console.log('unhandled grid action', event);
  }

  handleCancelAction() {
    this.cancelAction.emit();
  }

  handleSubmitAction() {
    console.log('Submit');
  }
}
