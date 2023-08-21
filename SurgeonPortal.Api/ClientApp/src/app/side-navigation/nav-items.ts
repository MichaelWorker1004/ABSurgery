export const TRAINEE_NAV_ITEMS = [
  {
    display: 'Dashboard',
    action: 'dashboard',
    icon: 'fa-solid fa-chart-line',
  },
  {
    display: 'Personal Profile',
    action: 'personal-profile',
    icon: 'fa-solid fa-circle-info',
  },
  {
    display: 'Medical Training',
    action: 'medical-training',
    icon: 'fa-solid fa-graduation-cap',
  },
  {
    display: 'GME',
    action: 'gme-history',
    icon: 'fa-regular fa-folder-open',
  },
  {
    display: 'Documents',
    action: 'documents',
    icon: 'fa-solid fa-file-lines',
  },
];

export const CERTIFIED_NAV_ITEMS = [
  {
    display: 'Dashboard',
    action: 'dashboard',
    icon: 'fa-solid fa-chart-line',
  },
  {
    display: 'Personal Profile',
    action: 'personal-profile',
    icon: 'fa-solid fa-circle-info',
  },
  {
    display: 'Medical Training',
    action: 'medical-training',
    icon: 'fa-solid fa-graduation-cap',
  },
  {
    display: 'Professional Standing',
    action: 'professional-standing',
    icon: 'fa-solid fa-stethoscope',
  },
  {
    display: 'CME Repository',
    action: 'cme-repository',
    icon: 'fa-regular fa-folder-open',
  },
  {
    display: 'Apply & Register',
    action: 'apply-and-resgister',
    icon: 'fa-solid fa-list-check',
    children: [
      {
        display: 'Registration Requirements',
        action: 'registration-requirements',
      },
      {
        display: 'Exam Registration',
        action: 'exam-registration',
      },
    ],
  },
  {
    display: 'Examination History',
    action: 'examination-history',
    icon: 'fa-sharp fa-solid fa-file-waveform',
  },
  {
    display: 'Continuous Certification',
    action: 'continuous-certification',
    icon: 'fa-solid fa-user-graduate',
  },
  {
    display: 'Payment History',
    action: 'payment-history',
    icon: 'fa-regular fa-credit-card',
  },
  {
    display: 'Documents',
    action: 'documents',
    icon: 'fa-solid fa-file-lines',
  },
  {
    display: 'Examiner Dashboard',
    action: 'ce-scoring',
    icon: 'fa-solid fa-clock',
    children: [
      {
        display: 'Examination Case Rosters',
        action: 'examination-rosters',
      },
      {
        display: 'Deliver CE Exams',
        action: 'oral-examinations',
      },
      {
        display: 'Examination Scores',
        action: 'examination-scores',
      },
    ],
  },
];
