import { createPropertySelectors } from '@ngxs/store';
import {
  IRegistrationRequirements,
  RegistrationRequirementsState,
} from './registration-requirements.state';

export class ReqistrationRequirmentsSelectors {
  static slices = createPropertySelectors<IRegistrationRequirements>(
    RegistrationRequirementsState
  );
}
