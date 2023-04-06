import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

export const GRADUATE_MEDICAL_EDUCATION_GRID_COLS = [
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
];
