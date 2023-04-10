import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

export const HOSPOITAL_APPOINTMENTS_COLS = [
  AbsGrid.setTextDisplayCol('Practice Type', 'practiceType'),
  AbsGrid.setTextDisplayCol('Appt Type', 'apptType'),
  AbsGrid.setTextDisplayCol('Orginiztion Type', 'organizationType'),
  AbsGrid.setTextDisplayCol('City', 'city'),
  AbsGrid.setTextDisplayCol('State', 'state'),
  AbsGrid.setTextDisplayCol('Institution', 'institution'),
  AbsGrid.setTextDisplayCol('Other', 'other'),
  AbsGrid.setTextDisplayCol('Auth Official', 'authOfficial'),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa fa-edit'),
];
