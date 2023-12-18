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
  ).prependIcon('fa-solid fa-file-pdf'),
  AbsGrid.setTextDisplayCol('Document Type', 'documentType', true),
  AbsGrid.setFormattedDateCol('Upload Date', 'uploadedDateUtc', true),
  AbsGrid.setTextDisplayCol('Uploaded By', 'uploadedBy', true),
  AbsGrid.setCustomButtonCol('Download', 'id', 'fa-solid fa-download'),
];
