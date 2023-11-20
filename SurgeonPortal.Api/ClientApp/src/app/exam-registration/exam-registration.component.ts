import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { PayFeeComponent } from '../shared/components/pay-fee/pay-fee.component';

import { RadioButtonModule } from 'primeng/radiobutton';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'abs-exam-registration',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
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
  siteSelectionFormData = [
    {
      label: '',
      value: '',
      required: false,
      name: 'siteSelection',
      type: 'radio',
      size: 'col-12',
      options: [
        {
          label: 'November 9 - 11, 2022',
          value: '11/09/2022-11/11/2022',
        },
        {
          label: 'November 16 - 18, 2022',
          value: '11/16/2022-11/18/2022',
        },
        {
          label: 'November 29 - 30, 2022',
          value: '11/29/2022-11/30/2022',
        },
        {
          label: 'December 2 - 4, 2022',
          value: '12/02/2022-12/04/2022',
        },
        {
          label: 'December 15, 2022',
          value: '12/15/2022',
        },
      ],
    },
  ];
  payFeeData: any;
  paymentGridData = [
    {
      paymentDate: new Date('09/18/2015'),
      paymentAmount: 100,
      balanceRemaining: 285.0,
    },
  ];

  ngOnInit(): void {
    this.getPayFeeData();
  }

  onSiteSelectionChange(event: any) {
    this.siteSelectionFormData
      .filter((item) => item.name === event.target.name)
      .map((item) => {
        item.value = event.target.value;
      });
  }

  getPayFeeData() {
    this.payFeeData = {
      totalAmountOfFee: 285.0,
      totalAmountPaidDate: new Date('11/5/2022'),
      totalAmountPaid: 0.0,
      remainingBalance: 285.0,
    };
  }

  handleSiteSelectionSubmit() {
    console.log('unhandled submit', this.siteSelectionFormData);
  }
  handleDigitalSignatureChange($event: any) {
    console.log('unhandled signature change', $event);
  }

  handleDownloadForm() {
    console.log('unhandled Download Form');
  }
}
