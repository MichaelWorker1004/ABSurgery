import { AbsGrid } from '../abs-grid';

export const GRID_DEFAULT_COLS = [
  AbsGrid.setTextDisplayCol('Name', 'name', true),
  AbsGrid.setTextDisplayCol('Title', 'title'),
  AbsGrid.setFormattedDateCol('Date', 'date', true),
  AbsGrid.setCellCustomClass('Status', 'status', true),
];

export const GRID_DEFAULT_COLS_EXPAND = [
  AbsGrid.setTextDisplayCol('Name', 'name', true),
  AbsGrid.setTextDisplayCol('Title', 'title'),
  AbsGrid.setFormattedDateCol('Date', 'date', true),
  AbsGrid.setCellCustomClass('Status', 'status', true),
  AbsGrid.setExpandToggle('Expand', 'expanded'),
];