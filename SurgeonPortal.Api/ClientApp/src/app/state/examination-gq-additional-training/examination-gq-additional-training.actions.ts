import { IAdditionalTrainingModel } from '../../api';

export class GetAdditionalTrainingList {
  static readonly type =
    '[GQAdditionalTraining] get list of additional training';

  constructor(public userId: number) {}
}
export class GetAdditionalTrainingDetails {
  static readonly type =
    '[GQAdditionalTraining] get details of additional training';

  constructor(public trainingId: number) {}
}

export class UpdateAdditionalTraining {
  static readonly type =
    '[GQAdditionalTraining] Update an additional training record';

  constructor(public payload: IAdditionalTrainingModel) {}
}

export class CreateAdditionalTraining {
  static readonly type =
    '[GQAdditionalTraining] Create an additional training record';

  constructor(public payload: IAdditionalTrainingModel) {}
}
