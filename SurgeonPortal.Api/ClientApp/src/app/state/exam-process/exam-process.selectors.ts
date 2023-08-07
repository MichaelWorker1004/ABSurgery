import { Selector, createPropertySelectors } from '@ngxs/store';
import { IExamProcess, ExamProcessState } from './exam-process.state';

export class ExamProcessSelectors {
  static slices = createPropertySelectors<IExamProcess>(ExamProcessState);

  @Selector([ExamProcessState])
  static examProcessValues(state: IExamProcess): IExamProcess | undefined {
    if (state) {
      return {
        examDirectory: state.examDirectory,
      };
    }

    return;
  }
}
