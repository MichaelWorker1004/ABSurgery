import { createPropertySelectors, Selector } from '@ngxs/store';
import {
  IMedicalTraining,
  MedicalTrainingState,
} from './medical-training.state';

export class MedicalTrainingSelectors {
  static slices =
    createPropertySelectors<IMedicalTraining>(MedicalTrainingState);

  @Selector([MedicalTrainingState])
  static medicalTrainingValues(
    state: IMedicalTraining
  ): IMedicalTraining | undefined {
    if (state) {
      return {
        medicalTraining: state.medicalTraining,
        additionalTraining: state.additionalTraining,
        userCertificates: state.userCertificates,
        otherCertifications: state.otherCertifications,
        fellowships: state.fellowships,
      };
    }

    return;
  }
}
