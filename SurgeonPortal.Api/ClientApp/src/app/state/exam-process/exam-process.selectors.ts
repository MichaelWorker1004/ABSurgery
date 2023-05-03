import { Selector, createPropertySelectors } from '@ngxs/store';
import { IExamProcess, ExamProcessState } from './exam-process.state';
import { IExamProcessReadOnlyModel } from '../../api';

export class ExamProcessSelectors {
  static slices = createPropertySelectors<IExamProcess>(ExamProcessState);

  @Selector([ExamProcessState])
  static availableExams(
    state: IExamProcess
  ): IExamProcessReadOnlyModel[] | undefined {
    if (state?.availableExams?.length >= 0) {
      return state.availableExams;
    }
    return;
  }
}
