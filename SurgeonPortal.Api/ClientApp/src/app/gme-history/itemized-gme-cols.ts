import { AbsGrid } from '../shared/components/grid/abs-grid';

export const ITEMIZED_GME_COLS = [
  AbsGrid.setFormattedDateCol('From', 'from'),
  AbsGrid.setFormattedDateCol('To', 'to'),
  AbsGrid.setTextDisplayCol('Weeks', 'weeks'),
  AbsGrid.setTextDisplayCol('Program Name', 'programName'),
  AbsGrid.setTextDisplayCol('Affiliated Institute', 'affiliatedInstitute'),
  AbsGrid.setTextDisplayCol('Clinical Level', 'clinicalLevel'),
  AbsGrid.setTextDisplayCol('Description (Non-Surgical Only)', 'description'),
  AbsGrid.setTextDisplayCol('Intl. Rotation', 'intlRotation'),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa fa-edit'),
  AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa fa-trash'),
];
