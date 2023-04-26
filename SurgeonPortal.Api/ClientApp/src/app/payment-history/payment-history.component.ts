import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { GridComponent } from '../shared/components/grid/grid.component';
import { AlertComponent } from '../shared/components/alert/alert.component';
import { PAYMENT_HISTORY_COLS } from './payment-histroy-grid';

@Component({
  selector: 'abs-payment-history',
  imports: [CommonModule, GridComponent, AlertComponent],
  templateUrl: './payment-history.component.html',
  styleUrls: ['./payment-history.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class PaymentHistoryComponent implements OnInit {
  paymentHistoryData!: any;
  paymentHistoryCols = PAYMENT_HISTORY_COLS;
  paymentFooterData!: any;

  ngOnInit(): void {
    this.getPaymentHistoryData();
  }

  getPaymentHistoryData() {
    this.paymentHistoryData = [
      {
        invoiceNumber: '12345678910112',
        feeCode: 'VOET',
        description: 'Vasular Surgery Certifying Examination',
        totalBilled: '$1,700.00',
        paid: '$0.00',
        balance: '$1,700.00',
      },
      {
        invoiceNumber: '12345678910112',
        feeCode: 'VOET',
        description: 'Pediatric Surgery Qualifying Examination (PQ - 2022)',
        totalBilled: '$1,700.00',
        paid: '$0.00',
        balance: '$1,700.00',
      },
      {
        invoiceNumber: '12345678910112',
        feeCode: 'VOET',
        description: 'Pediatric Surgery Certifying Examination',
        totalBilled: '$1,700.00',
        paid: '$0.00',
        balance: '$1,700.00',
      },
      {
        invoiceNumber: '12345678910112',
        feeCode: 'VOET',
        description: 'Complex General Surgical Oncology Certifying Examination',
        totalBilled: '$1,700.00',
        paid: '$0.00',
        balance: '$1,700.00',
      },
      {
        invoiceNumber: '12345678910112',
        feeCode: 'VOET',
        description: 'Hand Surgery Certification Examination (HC - 2022)',
        totalBilled: '$1,700.00',
        paid: '$0.00',
        balance: '$1,700.00',
      },
    ];

    this.paymentFooterData = {
      date: new Date('10/10/2022'),
      amount: '$1,700.00',
    };
  }

  handlePaymentClick() {
    console.log('handlePaymentClick');
  }
}
