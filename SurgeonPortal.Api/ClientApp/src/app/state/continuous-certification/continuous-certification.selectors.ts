import { createPropertySelectors, Selector } from '@ngxs/store';
import {
  ContinuousCertificationState,
  IContinuousCertication,
} from './continuous-certification.state';

export class ContinuousCertificationSelectors {
  static slices = createPropertySelectors<IContinuousCertication>(
    ContinuousCertificationState
  );

  @Selector([ContinuousCertificationState])
  static GetOutcomeRegistries(
    state: IContinuousCertication
  ): IContinuousCertication | undefined {
    if (state) {
      return {
        outcomeRegistries: state.outcomeRegistries,
        attestations: state.attestations,
        errors: state.errors,
        continuousCertificationStatuses: state.continuousCertificationStatuses,
        refrenceFormGridData: state.refrenceFormGridData,
      };
    }
    return;
  }
}
