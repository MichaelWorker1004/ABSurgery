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
    title: 'Medical Training',
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
    title: 'Professional Standing',
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
    description: 'Upload your operative report from ACGME.',
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
      'Complete the history of your general surgery rotations for all training years. Your PD will be required to review and approve the rotations you enter in this section.',
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
      'Once you have completed your GME history, send a request to your PD to attest to your clinical skills.',
    action: {
      type: Action.dialog,
      action: 'programDirectorAttestationModal',
    },
    actionDisplay: 'View / Update my information',
    icon: 'fa-solid fa-user-check',
  },
  {
    id: 'QE_Certificates',
    title: 'Certificate/Document Uploads',
    description: 'Upload documents relevant to your GQE application.',
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
      'The application fee is required before you can register for the exam (there is a separate fee for sitting the examination).',
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
      'Confirm whether you will need special accommodations for the upcoming examination.',
    action: {
      type: Action.dialog,
      action: 'specialAccommodationsModal',
    },
    actionDisplay: 'View / Update my information',
    icon: 'fa-solid fa-star',
  },
  {
    id: 'Attestation',
    title: 'Your Attestation',
    description:
      'Electronically attest that you have read and agree to the Surgeon Agreement Regarding ABS Certification/Continuous Certification Process.',
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
      'Have you met the minimum application requirements for exam registration? Go to the Apply & Register page.',
    actionStyle: 'button',
    disabled: true,
    actionDisplay: 'Apply Now',
    icon: 'fa-solid fa-language',
  },
];
