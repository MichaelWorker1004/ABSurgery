import { AbsGrid } from '../shared/components/grid/abs-grid';

export const EXAM_HISTORY_COLS = [
  AbsGrid.setCellCustomClass(
    'Exam Name',
    'examinationName',
    false,
    'font-bold'
  ),
  AbsGrid.setFormattedDateCol('Date', 'examinationDate'),
  AbsGrid.setTextDisplayCol('Status', 'status'),
  AbsGrid.setTextDisplayCol('Results', 'result'),
  AbsGrid.setCustomButtonCol('Download', 'documentId', 'fa fa-download'),
];
