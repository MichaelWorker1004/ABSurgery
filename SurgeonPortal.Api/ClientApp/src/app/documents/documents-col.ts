import { AbsGrid } from '../shared/components/grid/abs-grid';

export const DOCUMENTS_COLS = [
  AbsGrid.setCellCustomStyle(
    'Document Name',
    'documentName',
    {
      color: '#1C827D',
      fontWeight: 'bold',
    },
    true
  ),
  AbsGrid.setTextDisplayCol('Document Type', 'documentType', true),
  AbsGrid.setFormattedDateCol('Upload Date', 'uploadedDateUtc', true),
  AbsGrid.setTextDisplayCol('Uploaded By', 'uploadedBy', true),
  AbsGrid.setCustomButtonCol('Download', 'download', 'fa-solid fa-download'),
  AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa-solid fa-trash'),
];
