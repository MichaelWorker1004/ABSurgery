import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

export const OTHER_CERTIFICATIONS_COLS = [
  AbsGrid.setTextDisplayCol('Certificate Type', 'certificateTypeName'),
  AbsGrid.setTextDisplayCol('Certificate Number', 'certificateNumber'),
  AbsGrid.setFormattedDateCol('Issue Date', 'issueDate'),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa-solid fa-pen-to-square'),
];
