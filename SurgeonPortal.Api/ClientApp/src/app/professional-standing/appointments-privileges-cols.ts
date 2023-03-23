import { AbsGrid } from '../shared/components/grid/abs-grid';

export const APPOINTMENTS_PRIVILEGES_COLS = [
  AbsGrid.setTextDisplayCol('Practice Type', 'practiceType'),
  AbsGrid.setTextDisplayCol('Appointment Type', 'appointmentType'),
  AbsGrid.setTextDisplayCol('Organization Type', 'organizationType'),
  AbsGrid.setTextDisplayCol('State', 'state'),
  AbsGrid.setTextDisplayCol('Institution', 'institution'),
  AbsGrid.setTextDisplayCol('Other', 'other'),
  AbsGrid.setTextDisplayCol('Official', 'official'),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa-solid fa-pen-to-square'),
  AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa-solid fa-trash'),
];
