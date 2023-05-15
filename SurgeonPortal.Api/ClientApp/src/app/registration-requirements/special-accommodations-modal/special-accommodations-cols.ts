import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

export const SPECIAL_ACCOMMODATIONS_COLS = [
  AbsGrid.setCellCustomStyle('File Name', 'fileName', {
    color: '#1C827D',
  }),
  AbsGrid.setFormattedDateCol('Upload Date', 'uploadDate'),
  AbsGrid.setTextDisplayCol('Type', 'type'),
  AbsGrid.setCustomButtonCol('Download', 'download', 'fa fa-download'),
  AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa fa-trash'),
];