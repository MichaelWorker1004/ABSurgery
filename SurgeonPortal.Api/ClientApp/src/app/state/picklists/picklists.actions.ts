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

export class GetPicklists {
  static readonly type = '[Picklists] Get all picklists';
  constructor(public countryCode?: string) {}
}
