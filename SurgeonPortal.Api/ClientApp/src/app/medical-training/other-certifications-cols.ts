import { AbsGrid } from 'src/app/shared/components/grid/abs-grid';

export const OTHER_CERTIFICATIONS_COLS = [
  AbsGrid.setTextDisplayCol('Certificate Type', 'certificateType', true),
  AbsGrid.setTextDisplayCol('Certificate Number', 'certificateNumber', true),
  AbsGrid.setFormattedDateCol('Issue Date', 'issueDate', true),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa-solid fa-pen-to-square'),
];
