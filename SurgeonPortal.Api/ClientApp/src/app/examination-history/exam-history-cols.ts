import { AbsGrid } from '../shared/components/grid/abs-grid';

export const EXAM_HISTORY_COLS = [
  AbsGrid.setCellCustomClass('Exam Name', 'examTitle', 'font-bold'),
  AbsGrid.setFormattedDateCol('Date', 'date'),
  AbsGrid.setTextDisplayCol('Status', 'status'),
  AbsGrid.setCellCustomClass('Results', 'result'),
  AbsGrid.setExpandToggle('Expand', 'expanded'),
];
