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
  constructor(public countryCode?: string) {}
}

export class GetAccreditedProgramInstitutionsList {
  static readonly type =
    '[Picklists] Get the accredited program insitutions list';
}

export class GetTrainingTypeList {
  static readonly type = '[Picklists] Get the Training Type list';
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
