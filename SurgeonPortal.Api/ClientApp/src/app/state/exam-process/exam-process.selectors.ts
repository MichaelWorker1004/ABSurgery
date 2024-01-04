import { Selector, createPropertySelectors } from '@ngxs/store';
import { IExamProcess, ExamProcessState } from './exam-process.state';
import { IExamOverviewReadOnlyModel } from 'src/app/api/models/examinations/exam-overview-read-only.model';

export class ExamProcessSelectors {
  static slices = createPropertySelectors<IExamProcess>(ExamProcessState);

  @Selector([ExamProcessState])
  static examProcessValues(state: IExamProcess): IExamProcess | undefined {
    if (state) {
      return {
        examDirectory: state.examDirectory,
        examFees: state.examFees,
        examFeeTransaction: state.examFeeTransaction,
        examFeeByExamId: state.examFeeByExamId,
        siteSelection: state.siteSelection,
      };
    }

    return;
  }

  @Selector([ExamProcessState])
  static upcomingExams(
    state: IExamProcess
  ): IExamOverviewReadOnlyModel[] | undefined {
    if (state.examDirectory && state.examDirectory.length > 0) {
      //sort by registration end date
      const upcomingExams = state.examDirectory.filter((exam) => {
        return new Date(exam.regEndDate).getTime() > new Date().getTime();
      });
      upcomingExams.sort((a, b) => {
        return (
          new Date(a.regEndDate).getTime() - new Date(b.regEndDate).getTime()
        );
      });

      return upcomingExams.slice(0, 2);
    }

    return;
  }
}
