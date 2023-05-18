import { AbsGrid } from '../shared/components/grid/abs-grid';

export const ITEMIZED_GME_COLS = [
  AbsGrid.setFormattedDateCol('From', 'startDate'),
  AbsGrid.setFormattedDateCol('To', 'endDate'),
  //AbsGrid.setTextDisplayCol('Weeks', 'weeks'),
  AbsGrid.setTextDisplayCol('Program Name', 'programName'),
  AbsGrid.setTextDisplayCol('Affiliated Institute', 'alternateInstitutionName'),
  AbsGrid.setTextDisplayCol('Clinical Level', 'clinicalLevel'),
  AbsGrid.setTextDisplayCol(
    'Description (Non-Surgical Only)',
    'nonSurgicalActivity'
  ),
  AbsGrid.setYesNoDisplayCol('Intl. Rotation', 'isInternationalRotation'),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa fa-edit'),
  AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa fa-trash'),
];
