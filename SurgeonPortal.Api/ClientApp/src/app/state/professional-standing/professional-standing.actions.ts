import {
  IMedicalLicenseModel,
  ISanctionsModel,
  IUserAppointmentModel,
  IUserProfessionalStandingModel,
} from '../../api';

export class GetPSMedicalLicenseList {
  static readonly type = '[Professional Standing] get list of medical licenses';

  //constructor() {}
}

export class GetPSMedicalLicenseDetails {
  static readonly type =
    '[Professional Standing] get details of medical license by id';

  constructor(public id: number) {}
}

export class CreatePSMedicalLicense {
  static readonly type =
    '[Professional Standing] create new medical license record';

  constructor(public license: IMedicalLicenseModel) {}
}

export class UpdatePSMedicalLicense {
  static readonly type =
    '[Professional Standing] update existing medical license record';

  constructor(public license: IMedicalLicenseModel) {}
}

export class DeletePSMedicalLicense {
  static readonly type =
    '[Professional Standing] delete existing medical license record';

  constructor(public id: number) {}
}

export class GetPSAppointmentsAndPrivilegesList {
  static readonly type = '[Professional Standing] get list of appointments';

  //constructor() {}
}

export class GetPSAppointmentAndPrivilegeDetails {
  static readonly type =
    '[Professional Standing] get details of appointment by id';

  constructor(public id: number) {}
}

export class CreatePSAppointmentAndPrivilege {
  static readonly type =
    '[Professional Standing] create new appointment record';

  constructor(public data: IUserAppointmentModel) {}
}

export class UpdatePSAppointmentAndPrivilege {
  static readonly type =
    '[Professional Standing] update existing appointment record';

  constructor(public data: IUserAppointmentModel) {}
}

export class DeletePSAppointmentAndPrivilege {
  static readonly type =
    '[Professional Standing] delete existing appointment record';

  constructor(public id: number) {}
}

export class GetUserProfessionalStandingDetails {
  static readonly type =
    '[Professional Standing] get user professional standing details';
}

export class CreateUserProfessionalStandingDetails {
  static readonly type =
    '[Professional Standing] create user professional standing details';

  constructor(public details: IUserProfessionalStandingModel) {}
}

export class UpdateUserProfessionalStandingDetails {
  static readonly type =
    '[Professional Standing] update user professional standing details';

  constructor(public details: IUserProfessionalStandingModel) {}
}

export class GetProfessionalStandingSanctionsDetails {
  static readonly type =
    '[Professional Standing] get professional standing sanctions details';
}

export class CreateProfessionalStandingSanctionsDetails {
  static readonly type =
    '[Professional Standing] create professional standing sanctions details';

  constructor(public data: ISanctionsModel) {}
}

export class UpdateProfessionalStandingSanctionsDetails {
  static readonly type =
    '[Professional Standing] update professional standing sanctions details';

  constructor(public data: ISanctionsModel) {}
}

export class ClearProfessionalStandingErrors {
  static readonly type = '[Professional Standing] Clear Erros';
}
