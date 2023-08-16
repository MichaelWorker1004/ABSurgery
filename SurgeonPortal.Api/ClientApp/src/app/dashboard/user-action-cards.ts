export const TRAINEE_ACTION_CARDS = [
  {
    title: 'Graduate Medical Education (GME)',
    description:
      'Add rotations to your GME history. Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
    action: {
      type: 'component',
      action: '/gme-history',
    },
    actionDisplay: 'View Your GME',
    icon: 'fa-sharp fa-solid fa-file-waveform',
  },
  {
    title: 'Apply for a Qualified Exam',
    description: 'QE applications are not yet available. Check back on ',
    action: {
      type: 'component',
      action: '/exam-process/exam-registration',
    },
    actionDisplay: 'Coming Soon',
    icon: 'fa-solid fa-user-graduate',
    disabled: true,
  },
];

export const CERTIFIED_ACTION_CARDS = [
  {
    title: 'Continuous Certification Requirements',
    description:
      'This is the mailing address that we currently have on record for you. You receive any paper communications from us this way.',
    action: {
      type: 'component',
      action: '/continuous-certification',
    },
    actionDisplay: 'See Requirements',
    icon: 'fa-solid fa-user-graduate',
  },
  {
    title: 'Apply for an Exam',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
    action: {
      type: 'component',
      action: '/exam-process/exam-registration',
    },
    actionDisplay: 'Apply Now',
    icon: 'fa-solid fa-list-check',
  },
  {
    title: 'Register for an Exam or Assessment',
    description:
      'This is basic information like your first and last name, title, etc.',
    action: {
      type: 'component',
      action: '/exam-process',
    },
    actionDisplay: 'Register For an Exam Now',
    icon: 'fa-sharp fa-solid fa-file-waveform',
  },
];
