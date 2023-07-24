import { Selector, createPropertySelectors } from '@ngxs/store';
import { IExamHistory, ExamHistoryState } from './exam-history.state';

export class ExamHistorySelectors {
  static slices = createPropertySelectors<IExamHistory>(ExamHistoryState);

  @Selector([ExamHistoryState])
  static medicalTrainingValues(state: IExamHistory): IExamHistory | undefined {
    if (state) {
      return {
        examHistory: state.examHistory,
      };
    }

    return;
  }
}
