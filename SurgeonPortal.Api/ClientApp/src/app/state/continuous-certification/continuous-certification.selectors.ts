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
        continuousCertificationStatuses: state.continuousCertificationStatuses,
      };
    }
    return;
  }
}
