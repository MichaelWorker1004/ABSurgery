import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { GridComponent } from '../shared/components/grid/grid.component';
import { AlertComponent } from '../shared/components/alert/alert.component';
import { PAYMENT_HISTORY_COLS } from './payment-histroy-grid';
import { ButtonModule } from 'primeng/button';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { PayFeeComponent } from '../shared/components/pay-fee/pay-fee.component';
import { BehaviorSubject } from 'rxjs';

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
  paymentHistoryData!: any;
  paymentHistoryCols = PAYMENT_HISTORY_COLS;
  paymentFooterData!: any;
  payFeeModal = false;
  amount$: BehaviorSubject<number> = new BehaviorSubject(0);
  invoiceNumber$: BehaviorSubject<string> = new BehaviorSubject('');

  ngOnInit(): void {
    this.getPaymentHistoryData();
  }

  getPaymentHistoryData() {
    this.paymentHistoryData = [
      {
        invoiceNumber: '12345678910112',
        feeCode: 'VOET',
        description: 'Vasular Surgery Certifying Examination',
        totalBilled: 1700,
        paid: 0,
        balanceDue: 1700,
      },
      {
        invoiceNumber: '12344444444444',
        feeCode: 'VOET',
        description: 'Pediatric Surgery Qualifying Examination (PQ - 2022)',
        totalBilled: 1700,
        paid: 0,
        balanceDue: 1700,
      },
      {
        invoiceNumber: '555555555555',
        feeCode: 'VOET',
        description: 'Pediatric Surgery Certifying Examination',
        totalBilled: 1700,
        paid: 0,
        balanceDue: 0,
      },
      {
        invoiceNumber: '6666666666666',
        feeCode: 'VOET',
        description: 'Complex General Surgical Oncology Certifying Examination',
        totalBilled: 1700,
        paid: 0,
        balanceDue: 1700,
      },
      {
        invoiceNumber: '7777777777777',
        feeCode: 'VOET',
        description: 'Hand Surgery Certification Examination (HC - 2022)',
        totalBilled: 1700,
        paid: 0,
        balanceDue: 1700,
      },
    ];

    const totalBalance = this.paymentHistoryData.reduce(
      (acc: number, curr: any) => {
        return acc + curr.balanceDue;
      },
      0
    );

    this.paymentFooterData = {
      date: new Date(),
      amount: totalBalance,
    };
  }

  handleClosePayFee() {
    this.payFeeModal = !this.payFeeModal;
  }

  handlePaymentClick($event: any) {
    this.amount$.next($event.data.balanceDue);
    this.invoiceNumber$.next($event.data.invoiceNumber);
    this.payFeeModal = true;
  }
}
