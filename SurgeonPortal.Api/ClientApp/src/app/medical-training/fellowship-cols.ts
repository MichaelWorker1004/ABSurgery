import { AbsGrid } from '../shared/components/grid/abs-grid';

export const FELLOWSHIP_COLS = [
  AbsGrid.setTextDisplayCol('Program Name', 'programName', true),
  AbsGrid.setTextDisplayCol('Other', 'programOther', true),
  AbsGrid.setTextDisplayCol('Completion Year', 'completionYear', true),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa-solid fa-pen-to-square'),
  // AbsGrid.setCustomButtonCol('Delete', 'delete', 'fa-solid fa-trash-can'),
];
