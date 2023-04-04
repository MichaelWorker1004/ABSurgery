import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { Status } from '../shared/components/action-card/status.enum';
import { GridComponent } from '../shared/components/grid/grid.component';
import { PAY_FEE_COLS } from '../shared/components/pay-fee/pay-fee-cols';
import { REFERENCE_FORMS_COLS } from './refrence-forms-cols';
import { PayFeeComponent } from '../shared/components/pay-fee/pay-fee.component';

interface ActionMap {
  [key: string]: () => void;
}

@Component({
  selector: 'abs-continuous-certification',
  templateUrl: './continuous-certification.component.html',
  styleUrls: ['./continuous-certification.component.scss'],
  imports: [
    CommonModule,
    ActionCardComponent,
    GridComponent,
    FormsModule,
    PayFeeComponent,
  ],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ContinuousCertificationComponent implements OnInit {
  userData!: any;
  continousCertificationData!: any;
  outcomeRegistriesModal = false;
  attestationModal = false;
  referenceFormsModal = false;
  payFeeModal = false;
  referenceFormsCols = REFERENCE_FORMS_COLS;
  payFeeCols = PAY_FEE_COLS;
  payFeeData!: any;
  payFeeModalDescription = '';
  outcomesandRegistriesFormFields = [
    {
      label: 'Surgeeon Specific Registry (case log)',
      subLabel: '(ACS; with 30-day complications reporting)',
      value: '',
      required: false,
      name: 'surgeonSpecificRegistry',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },
    {
      label: 'Abdominal Core Health Quality Collaborative',
      subLabel: '(ACHQC)',
      value: '',
      required: false,
      name: 'abdominalCoreHealthQualityCollaborative',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },

    {
      label: 'Collaborative Endocrine Surgery Quality Improvement Program',
      subLabel: '(CESQIP)',
      value: '',
      required: false,
      name: 'collaborativeEndocrineSurgeryQualityImprovementProgram',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },
    {
      label:
        'Metabolic and Bariatric Surgery Accreditation and Quality Improvement Program (MBSAQIP)',
      subLabel: '(ACS)',
      value: '',
      required: false,
      name: 'metabolicAndBariatricSurgeryAccreditationAndQualityImprovementProgram',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },

    {
      label: 'National Burn Repository',
      subLabel: '(ABA)',
      value: '',
      required: false,
      name: 'nationalBurnRepository',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },
    {
      label: 'Mastery of Breast Surgery',
      subLabel: '(ASBS)',
      value: '',
      required: false,
      name: 'masteryOfBreastSurgery',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },

    {
      label: 'Statewide Collaboratives',
      subLabel: '(MSQC, SCOAP, etc.)',
      value: '',
      required: false,
      name: 'statewideCollaboratives',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },
    {
      label: 'Multi-specialty Portfolio Program',
      subLabel: '(ABMS)',
      value: '',
      required: false,
      name: 'multiSpecialityPortfolioProgram',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },

    {
      label: 'National Cancer Data Base',
      subLabel: '(NCDB)',
      value: '',
      required: false,
      name: 'nationalCancerDataBase',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },
    {
      label: 'CoC Rapid Quality Reporting System',
      subLabel: '(RQRS)',
      value: '',
      required: false,
      name: 'cocRapidQualityReportingSystem',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },

    {
      label: 'National Surgical Quality Improvement Program',
      subLabel: '(ACS NSQIP or VASQIP; adult or pediatric)',
      value: '',
      required: false,
      name: 'nationalSurgicalQualityImprovementProgram',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },
    {
      label: 'National Trauma Data Bank',
      subLabel: '(NTDB)',
      value: '',
      required: false,
      name: 'nationalTraumaDataBase',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },

    {
      label: 'Society of Thoracic Surgeons National Database',
      subLabel: '(STS)',
      value: '',
      required: false,
      name: 'societyOfThoracicSurgeonsNationalDatabase',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },
    {
      label: 'Trauma Quality Improvement Program',
      subLabel: '(ACS TQIP; adult or pediatric)',
      value: '',
      required: false,
      name: 'traumaQualityImprovementProgram',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },

    {
      label: 'Organ Procurement and Transplantation Network',
      subLabel: '(UNOS)',
      value: '',
      required: false,
      name: 'organProcurementAndTransplantationNetwork',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },
    {
      label: 'Peripheral Vascular Intervention Registry',
      subLabel: '(NCDR)',
      value: '',
      required: false,
      name: 'peripheralVascularInterventionRegistry',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },

    {
      label: 'Vascular Quality Initiative',
      subLabel: '(SVS)',
      value: '',
      required: false,
      name: 'vascularQualityInitiative',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },
    {
      label: 'Extracorporeal Life Support Organization Registry',
      subLabel: '(ELSO)',
      value: '',
      required: false,
      name: 'extracorporealLifeSupportOrganizationRegistry',
      type: 'radio',
      size: 'col-6',
      options: [
        {
          label: 'Yes',
          value: 'yes',
        },
        {
          label: 'No',
          value: 'no',
        },
      ],
    },
    {
      label: 'Describe',
      subLabel:
        'NOTE: if you responded “No” to all of the choices above, you MUST describe your Part 4 activity in the space provided below.',
      value: '',
      required: false,
      name: 'describe',
      type: 'textarea',
      size: 'col-12',
    },
  ];

  attestationFormFields = [
    {
      label:
        'I hereby authorize any hospital or medical staff where I now have,have had, or have applied for medical staff privileges, and anymedical organization of which I am a member or to which I have applied for membership, and any person who may have information (including medical records, patient records, and reports of committees) which is deemed by ABS to be material to its evaluation of this application, to provide such information to representatives of the ABS. I agree that communications of any nature made to the ABS regarding this application may be made in confidence and shall not be made available to me under any circumstances. I hereby release from liability any hospital. medical staff, medical organization or person, and ABS and its representatives, for acts performed in connection with this application.',
      subLabel: '',
      value: '',
      required: false,
      name: 'attestation1',
      placeholder: '',
      type: 'checkbox',
      size: 'col-12',
    },
    {
      label:
        'I understand that the certificate I will be issued upon successful completion of the biennial Continuous Certification Assessment will be contingent upon my on-going active participation in the Continuous Certification Program as a whole. I recognize that 10-year certificates are no longer offered by the ABS, and that the biennial Continuous Certification Assessment is replacing the traditional 10-vear recertification examination.',
      subLabel: '',
      value: '',
      required: false,
      name: 'attestation2',
      placeholder: '',
      type: 'checkbox',
      size: 'col-12',
    },
  ];

  referenceFormFields = [
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
          label: 'Chief of Staff',
          value: 'chief',
        },
        {
          label: 'Medical Director',
          value: 'medical',
        },
        {
          label: 'Program Director',
          value: 'program',
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
          label: 'Option 1',
          value: 'option1',
        },
        {
          label: 'Option 2',
          value: 'option2',
        },
        {
          label: 'Option 3',
          value: 'option3',
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
      options: [
        {
          label: 'Alabama',
          value: 'AL',
        },
        {
          label: 'Alaska',
          value: 'AK',
        },
      ],
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
    {
      label: 'Name',
      subLabel: '',
      value: '',
      required: true,
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

  paymentGridData = [
    {
      paymentDate: new Date('09/18/2015'),
      paymentAmount: '$100',
      balanceRemaining: '$285.00',
    },
  ];

  private actionMap: ActionMap = {
    outcomeRegistriesModal: () => {
      this.outcomeRegistriesModal = !this.outcomeRegistriesModal;
    },
    attestationModal: () => {
      this.attestationModal = !this.attestationModal;
    },
    referenceFormsModal: () => {
      this.referenceFormsModal = !this.referenceFormsModal;
    },
    payFeeModal: () => {
      this.payFeeModal = !this.payFeeModal;
    },
  };

  ngOnInit(): void {
    this.getUserData();
    this.getContinuousCertificationData();
    this.getPayFeeData();
  }

  getUserData() {
    this.userData = {
      name: 'John Doe, M.D',
    };
  }

  getPayFeeData() {
    this.payFeeData = {
      totalAmountOfFee: '$285.00',
      totalAmountPaidDate: new Date('11/5/2022'),
      totalAmountPaid: '$0.00',
      remainingBalance: '$285.00',
    };
  }

  getContinuousCertificationData() {
    this.continousCertificationData = [
      {
        title: 'Personal Profile',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/personal-profile',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-address-card',
        status: Status.Completed,
      },
      {
        title: 'Outcomes Registries / Quality Assessment Programs',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'dialog',
          action: 'outcomeRegistriesModal',
        },
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-stethoscope',
        status: Status.Completed,
      },
      {
        title: 'Medical Training',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/medical-training',
        },
        actionDisplay: 'View / Update my training',
        icon: 'fa-solid fa-language',
        status: Status.Completed,
      },
      {
        title: 'Professional Standing',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/professional-standing',
        },
        actionDisplay: 'View / Update my activities',
        icon: 'fa-solid fa-certificate',
        status: Status.InProgress,
      },
      {
        title: 'CME Repository',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/cme-repository',
        },
        actionDisplay: 'View CMEs',
        icon: 'fa-solid fa-id-card-clip',
        status: Status.InProgress,
      },
      {
        title: 'Pay Fee',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'dialog',
          action: 'payFeeModal',
        },
        actionDisplay: 'View / Pay Fee',
        icon: 'fa-solid fa-language',
        status: Status.InProgress,
      },
      {
        title: 'Reference Forms',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'dialog',
          action: 'referenceFormsModal',
        },
        actionDisplay: 'View / Update my activities',
        icon: 'fa-solid fa-rectangle-list',
        status: Status.InProgress,
      },
      {
        title: 'Attestation',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'dialog',
          action: 'attestationModal',
        },
        disabled: false,
        actionDisplay: 'View / Update my information',
        icon: 'fa-solid fa-user-check',
        status: Status.InProgress,
      },
      {
        title: 'Apply for an Exam',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          style: 2,
        },
        disabled: true,
        actionDisplay: 'Apply Now',
        icon: 'fa-solid fa-language',
      },
    ];
  }

  handleCardAction(action: string) {
    const actionFunction = this.actionMap[action];
    if (actionFunction) {
      actionFunction();
    }
  }

  handleGridAction($event: any) {
    // do action
    console.log($event);
  }

  onSubmit(fields: Array<any>) {
    const formData: any = [];

    fields.forEach((field) => {
      formData[field.name] = field.value;
    });

    console.log(formData);
  }
}
