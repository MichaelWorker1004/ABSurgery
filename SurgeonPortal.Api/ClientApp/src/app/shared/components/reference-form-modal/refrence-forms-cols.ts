import { AbsGrid } from '../grid/abs-grid';

export const REFERENCE_FORMS_COLS = [
  AbsGrid.setTextDisplayCol('Reference Form', 'referenceFormId'),
  AbsGrid.setTextDisplayCol('Affiliated Institution', 'affiliatedInstitution'),
  AbsGrid.setTextDisplayCol(
    'Authenticating Official',
    'authenticatingOfficial'
  ),
  AbsGrid.setFormattedDateCol('Date', 'date'),
  AbsGrid.setCellCustomClass('Status', 'status'),
  AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa-solid fa-trash'),
  AbsGrid.setExpandToggle('View', 'view'),
];
