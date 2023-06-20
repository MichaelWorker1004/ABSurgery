export const ACTION_CARDS = [
  {
    title: 'Examination Rosters',
    description:
      'Review the examinations rosters over the next 3 days prior to delivering the oral examinations.',
    action: {
      type: 'component',
      action: '/ce-scoring/examination-rosters',
    },
    actionDisplay: 'Review Exam Rosters',
    icon: 'fa-solid fa-clipboard-user',
  },
  {
    title: 'Oral Examinations',
    description:
      'This is where you will deliver the oral examinations and score the candidates.',
    action: {
      type: 'component',
      action: '/ce-scoring/oral-examinations',
    },
    actionDisplay: 'Begin Oral Examinations',
    icon: 'fa-solid fa-person-chalkboard',
  },
  {
    title: 'Examination Scores',
    description:
      'You can review previously submitted scores and edit if needed.',
    action: {
      type: 'component',
      action: '/ce-scoring/examination-scores',
    },
    actionDisplay: 'Review Exam Scores',
    icon: 'fa-solid fa-clipboard-user',
  },
];
