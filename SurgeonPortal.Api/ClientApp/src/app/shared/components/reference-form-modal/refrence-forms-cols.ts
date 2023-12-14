import { AbsGrid } from '../grid/abs-grid';

export const REFERENCE_FORMS_COLS = [
  AbsGrid.setTextDisplayCol('Reference Form', 'id'),
  AbsGrid.setTextDisplayCol('Hospital', 'hosp'),
  AbsGrid.setTextDisplayCol('Authenticating Official', 'official'),
  AbsGrid.setFormattedDateCol('Date Sent', 'letterSent'),
  AbsGrid.setCellCustomClass('Status', 'statusDisplay', 'statusClass'),
  AbsGrid.setExpandToggle('View', 'view'),
];
