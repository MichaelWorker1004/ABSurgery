import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { GridComponent } from '../shared/components/grid/grid.component';
import { AlertComponent } from '../shared/components/alert/alert.component';
import { PAYMENT_HISTORY_COLS } from './payment-histroy-grid';
import { ButtonModule } from 'primeng/button';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { PayFeeComponent } from '../shared/components/pay-fee/pay-fee.component';
import { BehaviorSubject, Observable } from 'rxjs';
import { Select, Store } from '@ngxs/store';
import { ExamProcessSelectors, GetExamFees } from '../state';
import { IExamFeeReadOnlyModel } from '../api/models/billing/exam-fee-read-only.model';
import { PAY_FEE_COLS } from '../shared/components/pay-fee/pay-fee-cols';

@Component({
  selector: 'abs-payment-history',
  imports: [
    CommonModule,
    GridComponent,
    AlertComponent,
    ButtonModule,
    PayFeeComponent,
    ModalComponent,
  ],
  templateUrl: './payment-history.component.html',
  styleUrls: ['./payment-history.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class PaymentHistoryComponent implements OnInit {
  @Select(ExamProcessSelectors.slices.examFees) examFees$:
    | Observable<IExamFeeReadOnlyModel[]>
    | undefined;

  paymentHistoryData!: any;
  paymentHistoryCols = PAY_FEE_COLS;
  paymentFooterData!: any;
  payFeeModal = false;
  amount$: BehaviorSubject<number> = new BehaviorSubject(0);
  invoiceNumber$: BehaviorSubject<string> = new BehaviorSubject('');

  constructor(private _store: Store) {
    this._store.dispatch(new GetExamFees());
  }

  ngOnInit(): void {
    this.getPaymentHistoryData();
  }

  getPaymentHistoryData() {
    this.examFees$?.subscribe((data) => {
      const totalBalance = data.reduce((acc: number, curr: any) => {
        return acc + curr.balanceDue;
      }, 0);

      this.paymentFooterData = {
        date: new Date(),
        amount: totalBalance,
      };
    });
  }

  handleClosePayFee() {
    this.payFeeModal = !this.payFeeModal;
  }

  handlePaymentClick($event: any) {
    if ($event.fieldKey === 'pay balance') {
      this.amount$.next($event.data.balanceDue);
      this.invoiceNumber$.next($event.data.invoiceNumber);
      this.payFeeModal = true;
    } else if ($event.fieldKey === 'receipt') {
      window.open(
        `api/reports/invoice?invoiceNumber=${$event.data.invoiceNumber}`,
        '_blank'
      );
    }
  }
}
