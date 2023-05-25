import { IRotationModel } from '../../api';

export class GetGraduateMedicalEducationSummary {
  static readonly type = '[GME] get list of gme summary';

  //constructor() {}
}
export class GetGraduateMedicalEducationList {
  static readonly type = '[GME] get list of gme rotations';

  //constructor() {}
}
export class GetGraduateMedicalEducationDetails {
  static readonly type = '[GME] get details of gme rotation';

  constructor(public id: number) {}
}

export class UpdateGraduateMedicalEducation {
  static readonly type = '[GME] Update a gme rotation record';

  constructor(public payload: IRotationModel) {}
}

export class CreateGraduateMedicalEducation {
  static readonly type = '[GME] Create a gme rotation record';

  constructor(public payload: IRotationModel) {}
}

export class DeleteGraduateMedicalEducation {
  static readonly type = '[GME] Delete a gme rotation record';
  constructor(public payload: number) {}
}

export class ClearGraduateMedicalEducationErrors {
  static readonly type = '[GME] Clear Erros';
}
