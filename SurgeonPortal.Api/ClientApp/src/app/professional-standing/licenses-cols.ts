import { AbsGrid } from '../shared/components/grid/abs-grid';

export const LICENSES_COLS = [
  AbsGrid.setTextDisplayCol('State', 'issuingState'),
  AbsGrid.setTextDisplayCol('Number', 'licenseNumber'),
  AbsGrid.setTextDisplayCol('Type', 'licenseType'),
  AbsGrid.setFormattedDateCol('Issue Date', 'issueDate'),
  AbsGrid.setFormattedDateCol('Expire Date', 'expireDate'),
  AbsGrid.setTextDisplayCol('Reporting Org.', 'reportingOrganization'),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa-solid fa-pen-to-square'),
];
