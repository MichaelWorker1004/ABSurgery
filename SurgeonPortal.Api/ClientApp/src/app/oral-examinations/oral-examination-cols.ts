import { AbsGrid } from '../shared/components/grid/abs-grid';

export const ORAL_EXAMINATION_COLS = [
  AbsGrid.setTextDisplayCol('Examinee', 'fullName'),
  AbsGrid.setTextDisplayCol('Scheduled Time', 'examTime'),
  AbsGrid.setCustomPrimeButtonCol(
    'Start Examination',
    'startExam',
    undefined,
    'p-button-warning',
    undefined,
    undefined,
    337
  ),
];
