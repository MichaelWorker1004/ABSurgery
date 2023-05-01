import { Selector } from '@ngxs/store';
import {
  IGQAdditionalTraining,
  GQAdditionalTrainingState,
} from './examination-gq-additional-training.state';
import {
  IAdditionalTrainingModel,
  IAdditionalTrainingReadOnlyModel,
} from 'src/app/api';

export class GQAdditionalTrainingSelectors {
  @Selector([GQAdditionalTrainingState])
  static additionalTrainingList(
    state: IGQAdditionalTraining
  ): IAdditionalTrainingReadOnlyModel[] | undefined {
    if (state?.AdditionalTraining?.length > 0) {
      return state.AdditionalTraining;
    }
    return;
  }

  @Selector([GQAdditionalTrainingState])
  static selectedAdditionalTrainingDetails(
    state: IGQAdditionalTraining
  ): IAdditionalTrainingModel | undefined {
    if (state?.selectedAdditionalTraining) {
      return state.selectedAdditionalTraining;
    }
    return;
  }
}
