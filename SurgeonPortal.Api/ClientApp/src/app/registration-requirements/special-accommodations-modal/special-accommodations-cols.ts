import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

export const SPECIAL_ACCOMMODATIONS_COLS = [
  AbsGrid.setCellCustomStyle('File Name', 'fileName', {
    color: '#1C827D',
  }),
  AbsGrid.setFormattedDateCol('Upload Date', 'createdAtUtc'),
  AbsGrid.setTextDisplayCol('Type', 'accommodationID'),
  AbsGrid.setCustomButtonCol('Download', 'download', 'fa fa-download'),
];
