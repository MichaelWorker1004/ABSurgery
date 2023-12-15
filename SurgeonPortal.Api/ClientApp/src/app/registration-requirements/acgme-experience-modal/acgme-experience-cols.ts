import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

export const ACGME_EXPERIENCE_GRID_COLS = [
  AbsGrid.setCellCustomStyle('Document Name', 'documentName', {
    color: '#1C827D',
  }).prependIcon('fa-solid fa-file-pdf'),
  AbsGrid.setFormattedDateCol('Upload Date', 'uploadDateUtc'),
  AbsGrid.setCustomButtonCol('Download', 'download', 'fa fa-download'),
];
