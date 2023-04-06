import { AbsGrid } from '../components/grid/abs-grid';

export const BASIC_DOCUMENT_COLS = [
  AbsGrid.setFileLinkCol('File Name', 'fileName', 'fileType'),
  AbsGrid.setFormattedDateCol('Upload Date', 'uploadDate'),
  AbsGrid.setCustomButtonCol(
    'Download',
    'download',
    'fa-solid fa-download',
    undefined,
    undefined,
    150
  ),
  AbsGrid.setCustomButtonCol(
    'Delete',
    'delete',
    'fa-solid fa-trash',
    undefined,
    undefined,
    125
  ),
];
