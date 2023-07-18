import { AbsGrid } from '../shared/components/grid/abs-grid';

export const ITEMIZED_CME_COLS = [
  AbsGrid.setFormattedDateCol('Date Earned', 'date', true),
  AbsGrid.setFormattedDateCol('Expiration', 'creditExpDate', true),
  AbsGrid.setTextDisplayCol('Description', 'description'),
  AbsGrid.setTextDisplayCol('Category 1', 'credits', true),
  AbsGrid.setTextDisplayCol('SA Credits', 'creditsSA', true),
  AbsGrid.setTextDisplayCol('CME Direct', 'cmeDirect'),
];
