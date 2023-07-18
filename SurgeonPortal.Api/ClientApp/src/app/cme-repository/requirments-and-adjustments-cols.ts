import { AbsGrid } from '../shared/components/grid/abs-grid';

export const REQIUREMENTS_AND_ADJUSTMENTS_COLS = [
  AbsGrid.setFormattedDateCol('Date Earned', 'date', true),
  AbsGrid.setFormattedDateCol('Expiration Date', 'creditExpDate', true),
  AbsGrid.setTextDisplayCol('Description', 'description'), // potentially change to activityName
  AbsGrid.setTextDisplayCol('Category 1', 'credits', true),
  AbsGrid.setTextDisplayCol('SA Credits', 'creditsSA', true),
];
