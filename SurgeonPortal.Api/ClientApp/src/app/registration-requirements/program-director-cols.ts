import { AbsGrid } from '../shared/components/grid/abs-grid';

export const PROGRAM_DIRECTOR_COLS = [
  AbsGrid.setTextDisplayCol('Reference Form', 'id'),
  AbsGrid.setTextDisplayCol('Hospital', 'hosp'),
  AbsGrid.setTextDisplayCol('Authenticating Official', 'official'),
  AbsGrid.setFormattedDateCol('Date Sent', 'letterSent'),
];
