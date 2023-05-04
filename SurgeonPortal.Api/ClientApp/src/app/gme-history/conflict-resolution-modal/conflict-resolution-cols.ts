import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

export const CONFLICT_RESOLUTION_GRID_COLS = [
  AbsGrid.setFormattedDateCol('From', 'from'),
  AbsGrid.setFormattedDateCol('To', 'to'),
  AbsGrid.setTextDisplayCol('Weeks', 'weeks'),
  AbsGrid.setTextDisplayCol('Program Name', 'programName'),
  AbsGrid.setTextDisplayCol('Affiliated Institute', 'affiliatedInstitute'),
  AbsGrid.setTextDisplayCol('Clinical Level', 'clinicalLevel'),
  AbsGrid.setTextDisplayCol('Explain', 'explain'),
  AbsGrid.setTextDisplayCol(
    'Description (Non-Surgical Only)',
    'descriptionNonSurgicalOnly'
  ),
  AbsGrid.setTextDisplayCol('International Rotation', 'internationalRotation'),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa-solid fa-pen-to-square'),
  AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa fa-trash'),
];
