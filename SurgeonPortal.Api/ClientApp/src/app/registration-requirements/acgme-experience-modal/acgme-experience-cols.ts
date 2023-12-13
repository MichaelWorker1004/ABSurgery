import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

export const ACGME_EXPERIENCE_GRID_COLS = [
  AbsGrid.setCellCustomStyle('File Name', 'fileName', {
    color: '#1C827D',
  }),
  AbsGrid.setFormattedDateCol('Upload Date', 'uploadDate'),
  AbsGrid.setCustomButtonCol('Download', 'download', 'fa fa-download'),
];
