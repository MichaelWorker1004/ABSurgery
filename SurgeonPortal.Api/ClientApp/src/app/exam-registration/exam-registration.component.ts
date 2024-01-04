import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { PayFeeComponent } from '../shared/components/pay-fee/pay-fee.component';

import { RadioButtonModule } from 'primeng/radiobutton';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { Select, Store } from '@ngxs/store';
import {
  GetSiteSelctionList,
  IPickListItem,
  PicklistsSelectors,
} from '../state/picklists';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { IApplicationFeeReadOnlyModel } from '../api/models/billing/application-fee-read-only.model';
import {
  ExamProcessSelectors,
  GetApplicationFee,
  GetSiteSelection,
  SetSiteSelection,
} from '../state';

@UntilDestroy()
@Component({
  selector: 'abs-exam-registration',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CollapsePanelComponent,
    PayFeeComponent,
    RadioButtonModule,
    CheckboxModule,
    ButtonModule,
  ],
  templateUrl: './exam-registration.component.html',
  styleUrls: ['./exam-registration.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ExamRegistrationComponent implements OnInit {
  @Select(PicklistsSelectors.slices.siteSelectionPicklist)
  siteSelectionPicklist$: Observable<IPickListItem[]> | undefined;

  @Select(ExamProcessSelectors.slices.applicationFee) applicationFee$:
    | Observable<IApplicationFeeReadOnlyModel[]>
    | undefined;

  @Select(ExamProcessSelectors.slices.siteSelection) siteSelection$:
    | Observable<string>
    | undefined;

  examRegistrationFormData = new FormGroup({
    siteSelection: new FormControl('', Validators.required),
  });

  siteSelectionPicklist!: any[];

  payFeeData: any;
  paymentGridData = [
    {
      paymentDate: new Date('09/18/2015'),
      paymentAmount: 100,
      balanceRemaining: 285.0,
    },
  ];

  constructor(private _store: Store, private route: ActivatedRoute) {
    this.route.params.pipe(untilDestroyed(this)).subscribe((params) => {
      const examHeaderId = params['examId'];
      if (examHeaderId) {
        this._store.dispatch(new GetSiteSelctionList(examHeaderId));
        this._store.dispatch(new GetApplicationFee(examHeaderId));
        this._store.dispatch(new GetSiteSelection(examHeaderId));
        this.getPayFeeData();
      }
    });
  }

  ngOnInit(): void {
    this.siteSelection$
      ?.pipe(untilDestroyed(this))
      .subscribe((siteSelection: string) => {
        if (siteSelection) {
          this.examRegistrationFormData.patchValue({
            siteSelection: siteSelection,
          });
        }
      });
  }

  getPayFeeData() {
    this.applicationFee$?.subscribe((examFees) => {
      const payFeeData = {
        totalAmountOfFee: 0,
        totalAmountPaidDate: new Date(),
        totalAmountPaid: 0,
        remainingBalance: 0,
      };

      examFees.forEach((examFee: IApplicationFeeReadOnlyModel) => {
        payFeeData.totalAmountOfFee += examFee.subTotal;
        payFeeData.totalAmountPaid += examFee.paidTotal;
        payFeeData.remainingBalance += examFee.balanceDue;
      });

      this.payFeeData = payFeeData;
    });
  }

  handleSiteSelectionSubmit() {
    console.log(
      'unhandled submit',
      this.examRegistrationFormData.getRawValue()
    );

    const siteSelection =
      this.examRegistrationFormData.getRawValue().siteSelection;

    if (siteSelection) {
      this._store.dispatch(new SetSiteSelection(siteSelection));
    }
  }

  handleDigitalSignatureChange($event: any) {
    console.log('unhandled signature change', $event);
  }

  handleDownloadForm() {
    console.log('unhandled Download Form');
  }
}
