import { AbsGrid } from '../components/grid/abs-grid';

export const MEDICAL_TRAINING_COLS = [
  AbsGrid.setTextDisplayCol('Type of Training', 'trainingType', true),
  AbsGrid.setTextDisplayCol('State', 'state', true),
  AbsGrid.setTextDisplayCol('City', 'city', true),
  AbsGrid.setTextDisplayCol('Institution', 'institutionName', true),
  AbsGrid.setTextDisplayCol('Other', 'other', true),
  AbsGrid.setFormattedDateCol('From', 'startDate', true),
  AbsGrid.setFormattedDateCol('To', 'endDate', true),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa-solid fa-pen-to-square'),
];
