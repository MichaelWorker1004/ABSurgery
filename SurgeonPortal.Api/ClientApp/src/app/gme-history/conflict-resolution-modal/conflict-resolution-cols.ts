import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

export const CONFLICT_RESOLUTION_GRID_COLS = [
  // AbsGrid.setFormattedDateCol('From', 'from'),
  // AbsGrid.setFormattedDateCol('To', 'to'),
  // AbsGrid.setTextDisplayCol('Weeks', 'weeks'),
  // AbsGrid.setTextDisplayCol('Program Name', 'programName'),
  // AbsGrid.setTextDisplayCol('Affiliated Institute', 'affiliatedInstitute'),
  // AbsGrid.setTextDisplayCol('Clinical Level', 'clinicalLevel'),
  // AbsGrid.setTextDisplayCol('Explain', 'explain'),
  // AbsGrid.setTextDisplayCol(
  //   'Description (Non-Surgical Only)',
  //   'descriptionNonSurgicalOnly'
  // ),
  // AbsGrid.setTextDisplayCol('International Rotation', 'internationalRotation'),
  // AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa-solid fa-pen-to-square'),
  // AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa fa-trash'),

  AbsGrid.setFormattedDateCol('From', 'startDate'),
  AbsGrid.setFormattedDateCol('To', 'endDate'),
  AbsGrid.setTextDisplayCol('Program Name', 'programName'),
  AbsGrid.setTextDisplayCol('Affiliated Institute', 'alternateInstitutionName'),
  AbsGrid.setTextDisplayCol('Clinical Level', 'clinicalLevel'),
  AbsGrid.setTextDisplayCol('Clinical Activity', 'clinicalActivity'),
  // AbsGrid.setTextDisplayCol(
  //   'Description (Non-Surgical Only)',
  //   'nonSurgicalActivity'
  // ),
  AbsGrid.setYesNoDisplayCol('Intl. Rotation', 'isInternationalRotation'),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa fa-edit'),
  // AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa fa-trash'),
];
