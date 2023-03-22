import { AbsGrid } from '../shared/components/grid/abs-grid';

export const REFERENCE_FORMS_COLS = [
  AbsGrid.setTextDisplayCol('Reference Form', 'referenceFormId'),
  AbsGrid.setTextDisplayCol('Affiliated Institution', 'affiliatedInstitution'),
  AbsGrid.setTextDisplayCol(
    'Authenticating Official',
    'authenticatingOfficial'
  ),
  AbsGrid.setFormattedDateCol('Date', 'date'),
  AbsGrid.setCellCustomClass('Status', 'status'),
  AbsGrid.setCustomButtonCol('View', 'view', 'fa-regular fa-eye'),
  AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa-solid fa-trash'),
];
