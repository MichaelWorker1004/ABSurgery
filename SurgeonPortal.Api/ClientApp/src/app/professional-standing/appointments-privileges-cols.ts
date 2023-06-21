import { AbsGrid } from '../shared/components/grid/abs-grid';

export const APPOINTMENTS_PRIVILEGES_COLS = [
  AbsGrid.setTextDisplayCol('Practice Type', 'practiceType'),
  AbsGrid.setTextDisplayCol('Appointment Type', 'appointmentType'),
  AbsGrid.setTextDisplayCol('Organization Type', 'organizationType'),
  AbsGrid.setTextDisplayCol('State', 'stateCode'),
  AbsGrid.setTextDisplayCol('Institution', 'organizationName'),
  AbsGrid.setTextDisplayCol('Other', 'other'),
  AbsGrid.setTextDisplayCol('Official', 'authorizingOfficial'),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa-solid fa-pen-to-square'),
  AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa-solid fa-trash'),
];
