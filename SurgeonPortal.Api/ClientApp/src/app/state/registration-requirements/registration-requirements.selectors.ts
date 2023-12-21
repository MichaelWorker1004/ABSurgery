import { Selector, createPropertySelectors } from '@ngxs/store';
import {
  IRegistrationRequirements,
  RegistrationRequirementsState,
} from './registration-requirements.state';

export class ReqistrationRequirmentsSelectors {
  static slices = createPropertySelectors<IRegistrationRequirements>(
    RegistrationRequirementsState
  );

  @Selector([RegistrationRequirementsState])
  static GetRegistrationRequirments(
    state: IRegistrationRequirements
  ): IRegistrationRequirements | undefined {
    if (state) {
      return {
        registrationRequirementsStatuses:
          state.registrationRequirementsStatuses,
        accommodation: state.accommodation,
        pdReferenceLetter: state.pdReferenceLetter,
        examTitle: state.examTitle,
        qeExamEligibility: state.qeExamEligibility,
        errors: state.errors,
      };
    }
    return;
  }
}
