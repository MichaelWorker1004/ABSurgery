export const ALL_NAV_ITEMS = [
  {
    display: 'Dashboard',
    displayKey: 'SIDENAV.DASHBOARD',
    action: 'dashboard',
    icon: 'fa-solid fa-chart-line',
    feature: 'dashboardPage',
    allowedClaims: ['user', 'trainee', 'surgeon', 'examiner'],
    order: 1,
  },
  {
    display: 'Personal Profile',
    displayKey: 'SIDENAV.PROFILE',
    action: 'personal-profile',
    icon: 'fa-solid fa-circle-info',
    feature: 'personalProfilePage',
    allowedClaims: ['user', 'trainee', 'surgeon', 'examiner'],
    order: 2,
  },
  {
    display: 'Medical Training',
    displayKey: 'SIDENAV.MEDICAL_TRAINING',
    action: 'medical-training',
    icon: 'fa-solid fa-graduation-cap',
    feature: 'medicalTrainingPage',
    allowedClaims: ['user', 'trainee', 'surgeon', 'examiner'],
    order: 3,
  },
  {
    display: 'Professional Standing',
    displayKey: 'SIDENAV.PROFESSIONAL_STANDING',
    action: 'professional-standing',
    icon: 'fa-solid fa-stethoscope',
    feature: 'professionalStandingPage',
    allowedClaims: ['surgeon'],
    order: 4,
  },
  {
    display: 'CME Repository',
    displayKey: 'SIDENAV.CME',
    action: 'cme-repository',
    icon: 'fa-regular fa-folder-open',
    feature: 'cmeRepositoryPage',
    allowedClaims: ['surgeon'],
    order: 5,
  },
  {
    display: 'GME',
    displayKey: 'SIDENAV.GME',
    action: 'gme-history',
    icon: 'fa-regular fa-folder-open',
    feature: 'gmeHistoryPage',
    allowedClaims: ['trainee'],
    order: 6,
  },
  {
    display: 'Apply & Register',
    displayKey: 'SIDENAV.APPLY_REGISTER.MAIN',
    action: 'apply-and-resgister',
    icon: 'fa-solid fa-list-check',
    feature: 'applyRegisterPage',
    allowedClaims: ['surgeon'],
    order: 7,
    children: [
      {
        display: 'Registration Requirements',
        displayKey: 'SIDENAV.APPLY_REGISTER.REQUIREMENTS',
        action: 'registration-requirements',
        allowedClaims: ['surgeon'],
        order: 7.1,
      },
      {
        display: 'Exam Registration',
        displayKey: 'SIDENAV.APPLY_REGISTER.REGISTRATION',
        action: 'exam-registration',
        allowedClaims: ['surgeon'],
        order: 7.2,
      },
    ],
  },
  {
    display: 'Examination History',
    displayKey: 'SIDENAV.EXAM_HISTORY',
    action: 'examination-history',
    icon: 'fa-sharp fa-solid fa-file-waveform',
    feature: 'examHistoryPage',
    allowedClaims: ['surgeon'],
    order: 8,
  },
  {
    display: 'Continuous Certification',
    displayKey: 'SIDENAV.CONTINUOUS_CERTIFICATION',
    action: 'continuous-certification',
    icon: 'fa-solid fa-user-graduate',
    feature: 'continuousCertificationPage',
    allowedClaims: ['surgeon'],
    order: 9,
  },
  {
    display: 'Payment History',
    displayKey: 'SIDENAV.PAYMENT_HISTORY',
    action: 'payment-history',
    icon: 'fa-regular fa-credit-card',
    feature: 'paymentHistoryPage',
    allowedClaims: ['surgeon'],
    order: 10,
  },
  {
    display: 'Documents',
    displayKey: 'SIDENAV.DOCUMENTS',
    action: 'documents',
    icon: 'fa-solid fa-file-lines',
    feature: 'documentsPage',
    allowedClaims: ['user', 'trainee', 'surgeon', 'examiner'],
    order: 11,
  },
  {
    display: 'CE Examiner Home',
    displayKey: 'SIDENAV.EXAM_SCORING.MAIN',
    action: 'ce-scoring',
    icon: 'fa-solid fa-clock',
    feature: 'examScoringPage',
    allowedClaims: ['examiner'],
    order: 12,
    children: [
      {
        display: 'Exam Case Rosters',
        displayKey: 'SIDENAV.EXAM_SCORING.CASE_ROSTER',
        action: 'examination-rosters',
        allowedClaims: ['examiner'],
        order: 12.1,
      },
      {
        display: 'Start CE Exams',
        displayKey: 'SIDENAV.EXAM_SCORING.EXAMINATION',
        action: 'oral-examinations',
        allowedClaims: ['examiner'],
        order: 12.2,
      },
      {
        display: 'Daily Exam Scores',
        displayKey: 'SIDENAV.EXAM_SCORING.SCORES',
        action: 'examination-scores',
        allowedClaims: ['examiner'],
        order: 12.3,
      },
    ],
  },
];
