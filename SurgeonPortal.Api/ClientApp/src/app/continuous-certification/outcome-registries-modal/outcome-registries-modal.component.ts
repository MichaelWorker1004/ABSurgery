import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'abs-outcome-registries-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './outcome-registries-modal.component.html',
  styleUrls: ['./outcome-registries-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class OutcomeRegistriesModalComponent {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

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

  close() {
    this.closeDialog.emit();
  }
}
