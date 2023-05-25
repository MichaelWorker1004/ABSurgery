import { Selector } from '@ngxs/store';
import {
  IGraduateMedicalEducation,
  GraduateMedicalEducationState,
} from './gme.state';
import {
  IRotationModel,
  IRotationReadOnlyModel,
  IGmeSummaryReadOnlyModel,
} from 'src/app/api';
import { IFormErrors } from 'src/app/shared/common';

export class GraduateMedicalEducationSelectors {
  @Selector([GraduateMedicalEducationState])
  static graduateMedicalEducationList(
    state: IGraduateMedicalEducation
  ): IRotationReadOnlyModel[] | undefined {
    if (state?.gmeRotations?.length > 0) {
      return state.gmeRotations;
    }
    return;
  }

  @Selector([GraduateMedicalEducationState])
  static graduateMedicalEducationSummary(
    state: IGraduateMedicalEducation
  ): IGmeSummaryReadOnlyModel[] | undefined {
    if (state?.gmeSummary?.length > 0) {
      return state.gmeSummary;
    }
    return;
  }

  @Selector([GraduateMedicalEducationState])
  static graduateMedicalEducationDetails(
    state: IGraduateMedicalEducation
  ): IRotationModel | undefined {
    if (state?.selectedRotation) {
      return state.selectedRotation;
    }
    return;
  }

  @Selector([GraduateMedicalEducationState])
  static errors(state: IGraduateMedicalEducation): IFormErrors | null {
    return <IFormErrors>state.errors;
  }
}
