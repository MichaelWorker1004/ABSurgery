import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

export const MEDICAL_LICENSE_GRID_COLS = [
  AbsGrid.setTextDisplayCol('License State', 'licenseState'),
  AbsGrid.setTextDisplayCol('License #', 'licenseNumber'),
  AbsGrid.setTextDisplayCol('License Type', 'licenseType'),
  AbsGrid.setFormattedDateCol('Issue Date', 'issueDate'),
  AbsGrid.setFormattedDateCol('Expiration Date', 'expirationDate'),
  AbsGrid.setTextDisplayCol('Verifying Organization', 'varifyingOrganization'),
  AbsGrid.setCustomButtonCol('View', 'view', 'fa fa-eye'),
];
