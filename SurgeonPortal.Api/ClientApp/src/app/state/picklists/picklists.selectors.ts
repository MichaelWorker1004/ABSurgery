import { createPropertySelectors, Selector } from '@ngxs/store';

import {
  IPicklist,
  IPicklistUserValues,
  PicklistsState,
} from './picklists.state';

export class PicklistsSelectors {
  static slices = createPropertySelectors<IPicklist>(PicklistsState);

  @Selector([PicklistsState])
  static userValues(state: IPicklist): IPicklistUserValues | undefined {
    if (state) {
      return {
        countries: state.countries,
        ethnicities: state.ethnicities,
        genders: state.genders,
        languages: state.languages,
        races: state.races,
        states: state.states,
        statesMap: state.statesMap,
      } as IPicklistUserValues;
    }
    return;
  }
}
