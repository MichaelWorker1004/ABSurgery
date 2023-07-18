import { Selector, createPropertySelectors } from '@ngxs/store';
import {
  IContinuingMedicalEducation,
  ContinuingMedicalEducationState,
  ICmeSummaryRow,
  ICmeAdjustment,
  ICmeCredit,
} from './cme.state';

export class ContinuingMedicalEducationSelectors {
  static slices = createPropertySelectors<IContinuingMedicalEducation>(
    ContinuingMedicalEducationState
  );

  @Selector([ContinuingMedicalEducationState])
  static continuingMedicalEducationCredits(
    state: IContinuingMedicalEducation
  ): ICmeCredit[] | undefined {
    if (state?.cmeCredits?.length > 0) {
      return state.cmeCredits;
    }
    return;
  }

  @Selector([ContinuingMedicalEducationState])
  static continuingMedicalEducationAdjustments(
    state: IContinuingMedicalEducation
  ): ICmeAdjustment[] | undefined {
    if (state?.cmeAdjustments?.length > 0) {
      return state.cmeAdjustments;
    }
    return;
  }

  @Selector([ContinuingMedicalEducationState])
  static continuingMedicalEducationSummary(
    state: IContinuingMedicalEducation
  ): ICmeSummaryRow[] | undefined {
    if (state?.cmeSummary?.length > 0) {
      return state.cmeSummary;
    }
    return;
  }
}
