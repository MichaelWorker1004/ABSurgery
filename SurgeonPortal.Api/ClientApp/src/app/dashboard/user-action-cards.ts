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
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
    action: {
      type: 'component',
      action: '/continuous-certification',
    },
    actionDisplay: 'See Requirements',
    icon: 'fa-solid fa-user-graduate',
  },
  {
    title: 'Apply and Register',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
    action: {
      type: 'component',
      action: '/apply-and-resgister',
    },
    actionDisplay: 'Apply Now',
    icon: 'fa-solid fa-list-check',
  },
  {
    title: 'Continuous Medical Education (CME)',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed neque nec dolor lacinia interdum.',
    action: {
      type: 'component',
      action: '/cme-repository',
    },
    actionDisplay: 'View CME Repository',
    icon: 'fa-sharp fa-solid fa-file-waveform',
  },
];
