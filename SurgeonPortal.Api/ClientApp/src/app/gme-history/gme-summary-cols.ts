import { AbsGrid } from '../shared/components/grid/abs-grid';

export const GME_SUMMARY_COLS = [
  AbsGrid.setTextDisplayCol('Clinical Level', 'clinicalLevel'),
  AbsGrid.setFormattedDateCol('From', 'minStartDate'),
  AbsGrid.setFormattedDateCol('To', 'maxStartDate'),
  AbsGrid.setTextDisplayCol('Program', 'programName'),
  AbsGrid.setTextDisplayCol('Clinical', 'clinicalWeeks'),
  AbsGrid.setTextDisplayCol('Non-Clinical', 'nonClinicalWeeks'),
  AbsGrid.setTextDisplayCol('Essentials', 'essentialsWeeks'),
];
