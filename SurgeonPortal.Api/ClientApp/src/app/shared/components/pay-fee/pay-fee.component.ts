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
} from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { GetStateList, PicklistsSelectors } from 'src/app/state/picklists';
import { Observable } from 'rxjs';
import { IStateReadOnlyModel } from 'src/app/api';
import { Select, Store } from '@ngxs/store';
import { IFormFields } from '../../models/form-fields/form-fields';
import { ButtonModule } from 'primeng/button';
import {
  ExamFeeTransaction,
  IUserProfile,
  UserProfileSelectors,
} from 'src/app/state';
import { PAY_FEE_FORM_FIELDS } from './pay-fee-form-fields';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { IExamFeeTransactionModel } from 'src/app/api/models/billing/exam-fee-transaction.mode';
import { GlobalDialogService } from '../../services/global-dialog.service';
import { ReportService } from 'src/app/api/services/reports/reports.service';

@UntilDestroy()
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
  @Input() showGrid = true;

  @Input() amount$: Observable<number> | undefined;
  @Input() invoiceNumber$: Observable<string> | undefined;

  states: IStateReadOnlyModel[] = [];

  paymentInformationFormFields: IFormFields[] = PAY_FEE_FORM_FIELDS;
  payFeeCols = PAY_FEE_COLS;

  paymentInformationForm: FormGroup = new FormGroup({});

  payFeeDisabled = true;

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService,
    private reportService: ReportService
  ) {
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
          new FormControl(
            { value: field.value, disabled: field.disabled ?? false },
            field.validators
          )
        );
        resolve();
      });
    });

    await Promise.all(promises);
    this.populateFormFields();
  }

  populateFormFields() {
    this.user$?.pipe(untilDestroyed(this)).subscribe((user) => {
      for (const [key, value] of Object.entries(user)) {
        this.paymentInformationForm.get(key)?.setValue(value);
      }
      this.paymentInformationForm.get('email')?.setValue(user.emailAddress);
      this.paymentInformationForm.get('addressLine1')?.setValue(user.street1);
      this.paymentInformationForm.get('addressLine2')?.setValue(user.street2);
      this.paymentInformationForm
        .get('phoneNumber')
        ?.setValue(user.officePhoneNumber);
    });

    if (this.amount$) {
      this.amount$?.pipe(untilDestroyed(this)).subscribe((amount) => {
        this.paymentInformationForm.get('amount')?.setValue(amount);
      });
    }

    if (this.invoiceNumber$) {
      this.invoiceNumber$
        ?.pipe(untilDestroyed(this))
        .subscribe((invoiceNumber) => {
          this.paymentInformationForm
            .get('invoiceNumber')
            ?.setValue(invoiceNumber);
          this.payFeeDisabled = false;
        });
    }
  }

  setPicklists() {
    this.states$?.pipe(untilDestroyed(this)).subscribe((states) => {
      this.states = states;
      this.paymentInformationFormFields.filter((fields) => {
        if (fields.name === 'state') {
          fields.options = states;
        }
      });
    });
  }

  handleGridAction(event: any) {
    if (event.fieldKey === 'pay balance') {
      const invoiceData = event.data;
      this.paymentInformationForm
        .get('invoiceNumber')
        ?.setValue(invoiceData.invoiceNumber);
      this.paymentInformationForm
        .get('amount')
        ?.setValue(invoiceData.balanceDue);
      this.payFeeDisabled = false;
    } else if (event.fieldKey === 'receipt') {
      this.reportService.downloadInvoice(event.data.invoiceNumber);
    }
  }

  handleCancelAction() {
    this.cancelAction.emit();
  }

  handleSubmitAction() {
    this.globalDialogService.showLoading();
    const formFields = this.paymentInformationForm.getRawValue();
    const model: IExamFeeTransactionModel = {
      ...formFields,
      invoiceNumber: formFields.invoiceNumber,
      zipCode: formFields.zipCode.toString().replace(/\s/g, ''),
      quantity: '1',
      phoneNumber: formFields.phoneNumber.replace(/\D/g, ''),
      description: `Payment for ${formFields.invoiceNumber}`,
      costPerUnit: formFields.amount.toString(),
      amount: formFields.amount.toString(),
      callbackUrl: 'https://portal-dev.absurgery.org',
    };

    this._store.dispatch(new ExamFeeTransaction(model));
  }
}
