import { Selector, createPropertySelectors } from '@ngxs/store';
import {
  IProfessionalStanding,
  ProfessionalStandingState,
} from './professional-standing.state';

export class ProfessionalStandingSelectors {
  static slices = createPropertySelectors<IProfessionalStanding>(
    ProfessionalStandingState
  );
}
