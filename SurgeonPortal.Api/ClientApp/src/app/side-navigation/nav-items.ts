export const TRAINEE_NAV_ITEMS = [
  {
    display: 'Dashboard',
    displayKey: 'SIDENAV.DASHBOARD',
    action: 'dashboard',
    icon: 'fa-solid fa-chart-line',
    feature: 'dashboardPage',
  },
  {
    display: 'Personal Profile',
    displayKey: 'SIDENAV.PROFILE',
    action: 'personal-profile',
    icon: 'fa-solid fa-circle-info',
    feature: 'personalProfilePage',
  },
  {
    display: 'Medical Training',
    displayKey: 'SIDENAV.MEDICAL_TRAINING',
    action: 'medical-training',
    icon: 'fa-solid fa-graduation-cap',
    feature: 'medicalTrainingPage',
  },
  {
    display: 'GME',
    displayKey: 'SIDENAV.GME',
    action: 'gme-history',
    icon: 'fa-regular fa-folder-open',
    feature: 'gmeHistoryPage',
  },
  {
    display: 'Documents',
    displayKey: 'SIDENAV.DOCUMENTS',
    action: 'documents',
    icon: 'fa-solid fa-file-lines',
    feature: 'documentsPage',
  },
];

export const CERTIFIED_NAV_ITEMS = [
  {
    display: 'Dashboard',
    displayKey: 'SIDENAV.DASHBOARD',
    action: 'dashboard',
    icon: 'fa-solid fa-chart-line',
    feature: 'dashboardPage',
  },
  {
    display: 'Personal Profile',
    displayKey: 'SIDENAV.PROFILE',
    action: 'personal-profile',
    icon: 'fa-solid fa-circle-info',
    feature: 'personalProfilePage',
  },
  {
    display: 'Medical Training',
    displayKey: 'SIDENAV.MEDICAL_TRAINING',
    action: 'medical-training',
    icon: 'fa-solid fa-graduation-cap',
    feature: 'medicalTrainingPage',
  },
  {
    display: 'Professional Standing',
    displayKey: 'SIDENAV.PROFESSIONAL_STANDING',
    action: 'professional-standing',
    icon: 'fa-solid fa-stethoscope',
    feature: 'professionalStandingPage',
  },
  {
    display: 'CME Repository',
    displayKey: 'SIDENAV.CME',
    action: 'cme-repository',
    icon: 'fa-regular fa-folder-open',
    feature: 'cmeRepositoryPage',
  },
  {
    display: 'Apply & Register',
    displayKey: 'SIDENAV.APPLY_REGISTER.MAIN',
    action: 'apply-and-resgister',
    icon: 'fa-solid fa-list-check',
    feature: 'applyRegisterPage',
    children: [
      {
        display: 'Registration Requirements',
        displayKey: 'SIDENAV.APPLY_REGISTER.REQUIREMENTS',
        action: 'registration-requirements',
      },
      {
        display: 'Exam Registration',
        displayKey: 'SIDENAV.APPLY_REGISTER.REGISTRATION',
        action: 'exam-registration',
      },
    ],
  },
  {
    display: 'Examination History',
    displayKey: 'SIDENAV.EXAM_HISTORY',
    action: 'examination-history',
    icon: 'fa-sharp fa-solid fa-file-waveform',
    feature: 'examHistoryPage',
  },
  {
    display: 'Continuous Certification',
    displayKey: 'SIDENAV.CONTINUOUS_CERTIFICATION',
    action: 'continuous-certification',
    icon: 'fa-solid fa-user-graduate',
    feature: 'continuousCertificationPage',
  },
  {
    display: 'Payment History',
    displayKey: 'SIDENAV.PAYMENT_HISTORY',
    action: 'payment-history',
    icon: 'fa-regular fa-credit-card',
    feature: 'paymentHistoryPage',
  },
  {
    display: 'Documents',
    displayKey: 'SIDENAV.DOCUMENTS',
    action: 'documents',
    icon: 'fa-solid fa-file-lines',
    feature: 'documentsPage',
  },
];

export const EXAMINER_NAV_ITEMS = [
  {
    display: 'Examiner Dashboard',
    displayKey: 'SIDENAV.EXAM_SCORING.MAIN',
    action: 'ce-scoring',
    icon: 'fa-solid fa-clock',
    feature: 'examScoringPage',
    children: [
      {
        display: 'Examination Case Rosters',
        displayKey: 'SIDENAV.EXAM_SCORING.CASE_ROSTERS',
        action: 'examination-rosters',
      },
      {
        display: 'Deliver CE Exams',
        displayKey: 'SIDENAV.EXAM_SCORING.EXAMINATION',
        action: 'oral-examinations',
      },
      {
        display: 'Examination Scores',
        displayKey: 'SIDENAV.EXAM_SCORING.SCORES',
        action: 'examination-scores',
      },
    ],
  },
];
