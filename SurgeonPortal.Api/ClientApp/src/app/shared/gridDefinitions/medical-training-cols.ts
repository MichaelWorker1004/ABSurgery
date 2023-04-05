import { AbsGrid } from '../components/grid/abs-grid';

export const MEDICAL_TRAINING_COLS = [
  AbsGrid.setTextDisplayCol('Type of Training', 'type'),
  AbsGrid.setTextDisplayCol('State', 'state'),
  AbsGrid.setTextDisplayCol('City', 'city'),
  AbsGrid.setTextDisplayCol('Institution', 'institution'),
  AbsGrid.setTextDisplayCol('Other', 'other'),
  AbsGrid.setFormattedDateCol('From', 'from'),
  AbsGrid.setFormattedDateCol('To', 'to'),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa-solid fa-pen-to-square'),
];
