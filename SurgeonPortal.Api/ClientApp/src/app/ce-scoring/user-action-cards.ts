export const ACTION_CARDS = [
  {
    title: 'Examination Case Rosters',
    description:
      'Review the cases for the next certifying examination and add personal notes as needed. Your personal notes will display each time you present the case.',
    action: {
      type: 'component',
      action: '/ce-scoring/examination-rosters',
    },
    actionDisplay: 'Review Exam Case Rosters',
    icon: 'fa-solid fa-clipboard-user',
  },
  {
    title: 'Deliver Certifying Examinations',
    description:
      'Certifying examinations along with scoring capability can be found here on exam day.',
    action: {
      type: 'component',
      action: '/ce-scoring/oral-examinations',
    },
    actionDisplay: 'Begin Certifying Examinations',
    icon: 'fa-solid fa-person-chalkboard',
  },
  {
    title: 'Examination Scores',
    description:
      'Check the status of submitted scores and edit any that remain incomplete as soon as possible.',
    action: {
      type: 'component',
      action: '/ce-scoring/examination-scores',
    },
    actionDisplay: 'Review Exam Scores',
    icon: 'fa-solid fa-clipboard-user',
  },
];
