import { createPropertySelectors } from '@ngxs/store';
import { IExamScoring, ExamScoringState } from './exam-scoring.state';

export class ExamScoringSelectors {
  static slices = createPropertySelectors<IExamScoring>(ExamScoringState);
}
