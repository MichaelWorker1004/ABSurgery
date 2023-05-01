import { AbsGrid } from '../components/grid/abs-grid';

export const MEDICAL_TRAINING_COLS = [
  AbsGrid.setTextDisplayCol('Type of Training', 'typeOfTraining'),
  AbsGrid.setTextDisplayCol('State', 'state'),
  AbsGrid.setTextDisplayCol('City', 'city'),
  AbsGrid.setTextDisplayCol('Institution', 'institutionName'),
  AbsGrid.setTextDisplayCol('Other', 'other'),
  AbsGrid.setFormattedDateCol('From', 'dateStarted'),
  AbsGrid.setFormattedDateCol('To', 'dateEnded'),
  AbsGrid.setCustomButtonCol('Edit', 'edit', 'fa-solid fa-pen-to-square'),
];
