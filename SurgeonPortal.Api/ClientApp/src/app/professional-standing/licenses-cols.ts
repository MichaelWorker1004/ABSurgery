import { AbsGrid } from '../shared/components/grid/abs-grid';

export const LICENSES_COLS = [
  AbsGrid.setTextDisplayCol('State', 'state'),
  AbsGrid.setTextDisplayCol('Number', 'number'),
  AbsGrid.setTextDisplayCol('Type', 'type'),
  AbsGrid.setFormattedDateCol('Issue Date', 'issueDate'),
  AbsGrid.setFormattedDateCol('Expire Date', 'expireDate'),
  AbsGrid.setTextDisplayCol('Reporting Org.', 'reportingOrg'),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa-solid fa-pen-to-square'),
];
