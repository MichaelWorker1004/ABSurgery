import { AbsGrid } from '../shared/components/grid/abs-grid';

export const ITEMIZED_GME_COLS = [
  AbsGrid.setFormattedDateCol('From', 'startDate', true),
  AbsGrid.setFormattedDateCol('To', 'endDate', true),
  AbsGrid.setTextDisplayCol('Program Name', 'programName', true),
  AbsGrid.setTextDisplayCol(
    'Affiliated Institute',
    'alternateInstitutionName',
    true
  ),
  AbsGrid.setTextDisplayCol('Clinical Level', 'clinicalLevel', true),
  AbsGrid.setTextDisplayCol('Clinical Activity', 'clinicalActivity', true),
  // AbsGrid.setTextDisplayCol(
  //   'Description (Non-Surgical Only)',
  //   'nonSurgicalActivity'
  // ),
  AbsGrid.setYesNoDisplayCol('Intl. Rotation', 'isInternationalRotation', true),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa fa-edit'),
  AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa fa-trash'),
];
