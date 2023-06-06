export class GetCountryList {
  static readonly type = '[Picklists] Get the country list';
}

export class GetEthnicityList {
  static readonly type = '[Picklists] Get the ethnicity list';
}

export class GetGenderList {
  static readonly type = '[Picklists] Get the genders list';
}

export class GetLanguageList {
  static readonly type = '[Picklists] Get the languages list';
}

export class GetRaceList {
  static readonly type = '[Picklists] Get the Races list';
}

export class GetStateList {
  static readonly type = '[Picklists] Get the states list';
  constructor(public countryCode?: string | undefined) {}
}

export class GetAccreditedProgramInstitutionsList {
  static readonly type =
    '[Picklists] Get the accredited program insitutions list';
}

export class GetTrainingTypeList {
  static readonly type = '[Picklists] Get the Training Type list';
}

export class GetGraduateProfiles {
  static readonly type = '[Picklists] Get all graduate profiles';
}

export class GetDegrees {
  static readonly type = '[Picklists] Get all degrees';
}

export class GetFellowshipPrograms {
  static readonly type = '[Picklists] Get all Fellowship Programs';
}

export class GetResidencyPrograms {
  static readonly type = '[Picklists] Get all Fellowship Programs';
}

export class GetCertificateTypes {
  static readonly type = '[Picklists] Get all Certificate Types';
}

export class GetDocumentTypes {
  static readonly type = '[Picklists] Get all Document Types';
}

export class GetPicklists {
  static readonly type = '[Picklists] Get all picklists';

  constructor(public countryCode?: string | null | undefined) {}
}

export class GetClinicalLevelList {
  static readonly type = '[Picklists] Get the Clinical Level list';
}

export class GetClinicalActivityList {
  static readonly type = '[Picklists] Get the Clinical Activity list';
}
