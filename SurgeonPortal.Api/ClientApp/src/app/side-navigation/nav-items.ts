export const TRAINEE_NAV_ITEMS = [
  {
    display: 'Dashboard',
    action: 'dashboard',
    icon: 'fa-solid fa-chart-line',
    feature: 'dashboardPage',
  },
  {
    display: 'Personal Profile',
    action: 'personal-profile',
    icon: 'fa-solid fa-circle-info',
    feature: 'personalProfilePage',
  },
  {
    display: 'Medical Training',
    action: 'medical-training',
    icon: 'fa-solid fa-graduation-cap',
    feature: 'medicalTrainingPage',
  },
  {
    display: 'GME',
    action: 'gme-history',
    icon: 'fa-regular fa-folder-open',
    feature: 'gmeHistoryPage',
  },
  {
    display: 'Documents',
    action: 'documents',
    icon: 'fa-solid fa-file-lines',
    feature: 'documentsPage',
  },
];

export const CERTIFIED_NAV_ITEMS = [
  {
    display: 'Dashboard',
    action: 'dashboard',
    icon: 'fa-solid fa-chart-line',
    feature: 'dashboardPage',
  },
  {
    display: 'Personal Profile',
    action: 'personal-profile',
    icon: 'fa-solid fa-circle-info',
    feature: 'personalProfilePage',
  },
  {
    display: 'Medical Training',
    action: 'medical-training',
    icon: 'fa-solid fa-graduation-cap',
    feature: 'medicalTrainingPage',
  },
  {
    display: 'Professional Standing',
    action: 'professional-standing',
    icon: 'fa-solid fa-stethoscope',
    feature: 'professionalStandingPage',
  },
  {
    display: 'CME Repository',
    action: 'cme-repository',
    icon: 'fa-regular fa-folder-open',
    feature: 'cmeRepositoryPage',
  },
  {
    display: 'Apply & Register',
    action: 'apply-and-resgister',
    icon: 'fa-solid fa-list-check',
    feature: 'applyRegisterPage',
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
    feature: 'examHistoryPage',
  },
  {
    display: 'Continuous Certification',
    action: 'continuous-certification',
    icon: 'fa-solid fa-user-graduate',
    feature: 'continuousCertificationPage',
  },
  {
    display: 'Payment History',
    action: 'payment-history',
    icon: 'fa-regular fa-credit-card',
    feature: 'paymentHistoryPage',
  },
  {
    display: 'Documents',
    action: 'documents',
    icon: 'fa-solid fa-file-lines',
    feature: 'documentsPage',
  },
];

export const EXAMINER_NAV_ITEMS = [
  {
    display: 'Examiner Dashboard',
    action: 'ce-scoring',
    icon: 'fa-solid fa-clock',
    feature: 'examScoringPage',
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
