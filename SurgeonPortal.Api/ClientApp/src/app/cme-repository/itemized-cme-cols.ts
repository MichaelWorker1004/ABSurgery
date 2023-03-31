import { AbsGrid } from '../shared/components/grid/abs-grid';

export const ITEMIZED_CME_COLS = [
  AbsGrid.setFormattedDateCol('Date', 'date', true),
  AbsGrid.setTextDisplayCol('Description', 'description'),
  AbsGrid.setTextDisplayCol('Credits', 'credits', true),
  AbsGrid.setTextDisplayCol('CME Direct', 'cmeDirect'),
];
