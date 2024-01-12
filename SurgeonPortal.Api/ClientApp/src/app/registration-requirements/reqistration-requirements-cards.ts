import { Action } from '../shared/components/action-card/action.enum';

export const REGISTRATION_REQUIRMENTS_CARDS = [
  {
    id: 'Personal_Profile',
    title: 'Personal Profile',
    description: 'Review the personal information ABS has on file for you.',
    action: {
      type: Action.component,
      action: '/personal-profile',
    },
    actionDisplay: 'View / Update my information',
    icon: 'fa-solid fa-address-card',
  },
  {
    id: 'Training',
    title: 'Training',
    description:
      'Update your medical training records, including your residency and fellowship information.',
    action: {
      type: Action.component,
      action: '/medical-training',
    },
    actionDisplay: 'View / Update my information',
    icon: 'fa-language fa-solid',

    recievedOn: new Date('2021-01-01'),
  },
  {
    id: 'Professional_Standing',
    title: 'Professional Activities and Privileges',
    description: 'Update your hospital appointments and sanctions/ethics.',
    action: {
      type: Action.component,
      action: '/professional-standing',
      anchor: 'hospital-appointments',
    },
    actionDisplay: 'View / Update my information',
    icon: 'fa-solid fa-user-doctor',
  },
  {
    id: 'Medical_Licenses',
    title: 'Medical License',
    description: 'Review and Update your state medical license information.',
    action: {
      type: Action.component,
      action: '/professional-standing',
      anchor: 'medical-license',
    },
    actionDisplay: 'View / Update my license',
    icon: 'fa-certificate fa-solid',
  },
  {
    id: 'ACGME',
    title: 'ACGME Experience Report by Role',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
    action: {
      type: Action.dialog,
      action: 'ACGMEExperienceModal',
    },
    actionDisplay: 'View / Update my information',
    icon: 'fa-solid fa-stethoscope',
  },
  {
    id: 'GME',
    title: 'Graduate Medical Education (GME)',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
    action: {
      type: Action.component,
      action: '/gme-history',
    },
    actionDisplay: 'View / Update my information',
    icon: 'fa-solid fa-stethoscope',
  },
  {
    id: 'PD_Attestation',
    title: 'Program Director Attestation',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
    action: {
      type: Action.dialog,
      action: 'programDirectorAttestationModal',
    },
    actionDisplay: 'View / Update my information',
    icon: 'fa-solid fa-user-check',
  },
  {
    id: 'QE_Certificates',
    title: 'Certification(s) Upload',
    description: 'Please review and update your certification(s) information.',
    action: {
      type: Action.component,
      action: '/medical-training',
      anchor: 'certifications',
    },
    actionDisplay: 'View / Update my information',
    icon: 'fa-solid fa-rectangle-list',
  },
  {
    id: 'Application_Fee',
    title: 'Application Fee',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
    action: {
      type: Action.dialog,
      action: 'payFeeModal',
    },
    actionDisplay: 'View / Update my information',
    icon: 'fa-solid fa-cash-register',
  },
  {
    id: 'Special_Accommodations',
    title: 'Special Accommodations',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
    action: {
      type: Action.dialog,
      action: 'specialAccommodationsModal',
    },
    actionDisplay: 'View / Update my information',
    icon: 'fa-solid fa-star',
  },
  {
    id: 'Attestation',
    title: 'Attestation',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
    action: {
      type: Action.dialog,
      action: 'attestationModal',
    },
    actionDisplay: 'View / Update my Attesation',
    icon: 'fa-solid fa-star',
  },
  {
    id: 'applyForExam',
    title: 'Apply for an Exam',
    action: {
      style: 2,
      type: Action.action,
      action: 'applyForExam',
    },
    description:
      'All requirements up to date? Go to the Apply & Register page.',
    actionStyle: 'button',
    disabled: true,
    actionDisplay: 'Apply Now',
    icon: 'fa-solid fa-language',
  },
];
