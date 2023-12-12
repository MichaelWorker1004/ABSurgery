import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

export const SPECIAL_ACCOMMODATIONS_COLS = [
  AbsGrid.setCellCustomStyle('File Name', 'documentName', {
    color: '#1C827D',
  }),
  AbsGrid.setFormattedDateCol('Upload Date', 'createdAtUtc'),
  AbsGrid.setFormattedDateCol('Updated Date', 'lastUpdatedAtUtc'),
  AbsGrid.setTextDisplayCol('Type', 'accommodationName'),
  AbsGrid.setCustomButtonCol('Download', 'download', 'fa fa-download'),
];
