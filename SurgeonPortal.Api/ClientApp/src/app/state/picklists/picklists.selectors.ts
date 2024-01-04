import { createPropertySelectors, Selector } from '@ngxs/store';

import {
  IPicklist,
  IPicklistUserValues,
  PicklistsState,
} from './picklists.state';

export class PicklistsSelectors {
  static slices = createPropertySelectors<IPicklist>(PicklistsState);

  @Selector([PicklistsState])
  static userPicklistValues(state: IPicklist): IPicklistUserValues | undefined {
    if (state) {
      return {
        countries: state.countries,
        ethnicities: state.ethnicities,
        genders: state.genders,
        languages: state.languages,
        races: state.races,
        states: state.states,
        statesMap: state.statesMap,
        accreditedInstitutions: state.accreditedInstitutions,
        trainingTypes: state.trainingTypes,
        graduateProfiles: state.graduateProfiles,
        degrees: state.degrees,
        fellowshipPrograms: state.fellowshipPrograms,
        residencyPrograms: state.residencyPrograms,
        accommodationTypes: state.accommodationTypes,
        siteSelectionPicklist: state.siteSelectionPicklist,
      } as IPicklistUserValues;
    }
    return;
  }
}
