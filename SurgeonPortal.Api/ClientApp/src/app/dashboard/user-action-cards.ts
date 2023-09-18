export const TRAINEE_ACTION_CARDS = [
  {
    title: '',
    titleKey: 'DASHBOARD_GME_TITLE',
    description: '',
    descriptionKey: 'DASHBOARD_GME_SUBTITLE',
    action: {
      type: 'component',
      action: '/gme-history',
    },
    actionDisplay: '',
    actionDisplayKey: 'DASHBOARD_GME_BTN',
    icon: 'fa-sharp fa-solid fa-file-waveform',
  },
  {
    title: '',
    titleKey: 'DASHBOARD_APPLY_TITLE',
    description: '',
    descriptionKey: 'DASHBOARD_APPLY_SUBTITLE',
    action: {
      type: 'component',
      action: '/exam-process/exam-registration',
    },
    actionDisplay: '',
    actionDisplayKey: 'DASHBOARD_APPLY_BTN',
    icon: 'fa-solid fa-user-graduate',
    disabled: true,
  },
];

export const CERTIFIED_ACTION_CARDS = [
  {
    title: '',
    titleKey: 'DASHBOARD_CCR_TITLE',
    description: '',
    descriptionKey: 'DASHBOARD_CCR_SUBTITLE',
    action: {
      type: 'component',
      action: '/continuous-certification',
    },
    actionDisplay: '',
    actionDisplayKey: 'DASHBOARD_CCR_BTN',
    icon: 'fa-solid fa-user-graduate',
  },
  {
    title: '',
    titleKey: 'DASHBOARD_REGISTER_TITLE',
    description: '',
    descriptionKey: 'DASHBOARD_REGISTER_SUBTITLE',
    action: {
      type: 'component',
      action: '/apply-and-resgister',
    },
    actionDisplay: '',
    actionDisplayKey: 'DASHBOARD_REGISTER_BTN',
    icon: 'fa-solid fa-list-check',
  },
  {
    title: '',
    titleKey: 'DASHBOARD_CME_TITLE',
    description: '',
    descriptionKey: 'DASHBOARD_CME_SUBTITLE',
    action: {
      type: 'component',
      action: '/cme-repository',
    },
    actionDisplay: '',
    actionDisplayKey: 'DASHBOARD_CME_BTN',
    icon: 'fa-sharp fa-solid fa-file-waveform',
  },
];
