import { Selector, createPropertySelectors } from '@ngxs/store';
import { IExamScoring, ExamScoringState } from './exam-scoring.state';

export class ExamScoringSelectors {
  static slices = createPropertySelectors<IExamScoring>(ExamScoringState);

  @Selector([ExamScoringState])
  static examScoringValues(state: IExamScoring): any | undefined {
    if (state) {
      return {
        caseRoster: state.caseRoster,
        selectedCaseContents: state.selectedCaseContents,
        examineeList: state.examineeList,
        examinee: state.examinee,
        activeExamination: state.activeExamination,
        examScoresList: state.examScoresList,
        roster: state.roster,
        dashboardRoster: state.dashboardRoster,
      };
    }

    return;
  }
}
