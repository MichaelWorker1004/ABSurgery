import { AbsGrid } from '../shared/components/grid/abs-grid';

export const REQIUREMENTS_AND_ADJUSTMENTS_COLS = [
  AbsGrid.setFormattedDateCol('Date', 'date'),
  AbsGrid.setTextDisplayCol('Description', 'description'),
  AbsGrid.setTextDisplayCol('Category 1', 'category1'),
  AbsGrid.setTextDisplayCol('SA Credits', 'saCredits'),
  AbsGrid.setTextDisplayCol('Issued By', 'issuedBy'),
];
