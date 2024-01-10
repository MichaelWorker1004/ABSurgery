import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

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
  AbsGrid.setTextDisplayCol('Certificate Type', 'certificateType', true),
  AbsGrid.setFormattedDateCol('Upload Date', 'uploadDateUtc', true),
  AbsGrid.setCustomButtonCol('Download', 'documentId', 'fa-solid fa-download'),
  // AbsGrid.setCustomButtonCol('Delete', 'documentId', 'fa-solid fa-trash'),
];
