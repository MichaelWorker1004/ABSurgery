import { AbsGrid } from '../shared/components/grid/abs-grid';

export const DIRECTORY_COLS = [
  AbsGrid.setTextDisplayCol('Exam Title', 'examName'),
  AbsGrid.setFormattedDateCol('Registry Open', 'regOpenDate'),
  AbsGrid.setFormattedDateCol('Registry Close', 'regEndDate'),
  AbsGrid.setFormattedDateCol('Exam Start', 'examStartDate'),
  AbsGrid.setFormattedDateCol('Exam End', 'examEndDate'),
];
