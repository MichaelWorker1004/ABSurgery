import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';

interface ActionMap {
  [key: string]: () => void;
}

@Component({
  selector: 'abs-continuous-certification',
  templateUrl: './continuous-certification.component.html',
  styleUrls: ['./continuous-certification.component.scss'],
  imports: [CommonModule, ActionCardComponent],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ContinuousCertificationComponent implements OnInit {
  userData!: any;
  continousCertificationData!: any;
  outcomeRegistriesModal = false;
  outcomesandRegistriesFormFields = [
    {
      label: 'Surgeeon Specific Registry (case log)',
      subLabel: '(ACS; with 30-day complications reporting)',
      value: '',
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
      type: 'textarea',
      size: 'col-12',
    },
  ];

  private actionMap: ActionMap = {
    outcomeRegistriesModal: () => {
      this.outcomeRegistriesModal = !this.outcomeRegistriesModal;
    },
  };

  ngOnInit(): void {
    this.getUserData();
    this.getContinuousCertificationData();
  }

  getUserData() {
    this.userData = {
      name: 'John Doe, M.D',
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
        status: 'completed',
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
        status: 'completed',
      },
      {
        title: 'Medical Training',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/apply',
        },
        actionDisplay: 'View / Update my training',
        icon: 'fa-solid fa-language',
        status: 'completed',
      },
      {
        title: 'Professional Standing',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/apply',
        },
        actionDisplay: 'View / Update my activities',
        icon: 'fa-solid fa-certificate',
        status: 'in-progress',
      },
      {
        title: 'CME Repository',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/personal-profile',
        },
        actionDisplay: 'View CMEs',
        icon: 'fa-solid fa-id-card-clip',
        status: 'in-progress',
      },
      {
        title: 'Pay Fee',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/apply',
        },
        actionDisplay: 'View / Pay Fee',
        icon: 'fa-solid fa-language',
        status: 'in-progress',
      },
      {
        title: 'Reference Forms',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {
          type: 'component',
          action: '/apply',
        },
        actionDisplay: 'View / Update my activities',
        icon: 'fa-solid fa-rectangle-list',
        status: 'in-progress',
      },
      {
        title: 'Attestation',
        description:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
        action: {},
        disabled: true,
        actionDisplay: 'Not available yet',
        icon: 'fa-solid fa-user-check',
        status: 'contingent',
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
}
