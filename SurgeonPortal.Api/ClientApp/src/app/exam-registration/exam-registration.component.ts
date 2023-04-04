import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { PayFeeComponent } from '../shared/components/pay-fee/pay-fee.component';

@Component({
  selector: 'abs-exam-registration',
  standalone: true,
  imports: [CommonModule, CollapsePanelComponent, PayFeeComponent],
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
      paymentAmount: '$100',
      balanceRemaining: '$285.00',
    },
  ];

  ngOnInit(): void {
    this.getPayFeeData();
  }

  onSiteSelectionChange(event: any) {
    console.log(event.target.name);
    this.siteSelectionFormData
      .filter((item) => item.name === event.target.name)
      .map((item) => {
        item.value = event.target.value;
      });
  }

  getPayFeeData() {
    this.payFeeData = {
      totalAmountOfFee: '$285.00',
      totalAmountPaidDate: new Date('11/5/2022'),
      totalAmountPaid: '$0.00',
      remainingBalance: '$285.00',
    };
  }

  handleSiteSelectionSubmit() {
    console.log(this.siteSelectionFormData);
  }
  handleDigitalSignatureChange($event: any) {
    console.log($event);
  }

  handleDownloadForm() {
    console.log('Download Form');
  }
}
