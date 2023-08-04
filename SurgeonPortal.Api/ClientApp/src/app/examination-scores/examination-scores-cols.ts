import { AbsGrid } from '../shared/components/grid/abs-grid';

export const EXAMINATION_SCORES_COLS = [
  AbsGrid.setTextDisplayCol('Day', 'day', true),
  AbsGrid.setTextDisplayCol('Session', 'session', true),
  AbsGrid.setTextDisplayCol('Roster', 'roster', true),
  AbsGrid.setTextDisplayCol('Candidate Name', 'displayName', true),
  // AbsGrid.setTextDisplayCol('Score', 'score', true),
  // AbsGrid.setTextDisplayCol('Critical Fail Y/N', 'criticalFail', true),
  AbsGrid.setCellCustomClass('Status', 'status', true),
  AbsGrid.setCustomButtonCol('View', 'view', 'fa fa-eye'),
];
