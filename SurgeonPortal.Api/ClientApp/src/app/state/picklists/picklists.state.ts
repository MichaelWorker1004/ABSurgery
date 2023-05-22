import { Injectable } from '@angular/core';
import { catchError, share, tap } from 'rxjs/operators';
import { forkJoin, map, Observable, of } from 'rxjs';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';

import {
  ICountryReadOnlyModel,
  IEthnicityReadOnlyModel,
  IGenderReadOnlyModel,
  ILanguageReadOnlyModel,
  IRaceReadOnlyModel,
  IStateReadOnlyModel,
  IClinicalLevelReadOnlyModel,
  IClinicalActivityReadOnlyModel,
  PicklistsService,
} from '../../api';
import {
  GetAccreditedProgramInstitutionsList,
  GetCountryList,
  GetEthnicityList,
  GetGenderList,
  GetLanguageList,
  GetPicklists,
  GetRaceList,
  GetStateList,
  GetTrainingTypeList,
  GetClinicalLevelList,
  GetClinicalActivityList,
} from './picklists.actions';
import { IFormErrors } from '../../shared/common';
import { IAccreditedProgramInstitutionReadOnlyModel } from 'src/app/api/models/picklists/accredited-program-institution-read-only.model';
import { ITrainingTypeReadOnlyModel } from 'src/app/api/models/picklists/training-type-read-only.model';
export interface IPicklist {
  countries: ICountryReadOnlyModel[] | undefined;
  ethnicities: IEthnicityReadOnlyModel[] | undefined;
  genders: IPickListItem[] | undefined;
  languages: IPickListItem[] | undefined;
  races: IRaceReadOnlyModel[] | undefined;
  states: IStateReadOnlyModel[] | undefined;
  statesMap: { [key: string]: IStateReadOnlyModel[] };
  defaultStates: IStateReadOnlyModel[] | undefined;
  accreditedInstitutions:
    | IAccreditedProgramInstitutionReadOnlyModel[]
    | undefined;
  trainingTypes: ITrainingTypeReadOnlyModel[] | undefined;
  clinicalLevels: IClinicalLevelReadOnlyModel[] | undefined;
  clinicalActivities: IClinicalActivityReadOnlyModel[] | undefined;
  errors?: IFormErrors | undefined;
}

export interface IPickListItem {
  itemValue: string | null | undefined;
  itemDescription: string | null | undefined;
  modifier?: string | null | undefined;
  isCredit?: boolean | null | undefined;
  isEssential?: boolean | null | undefined;
}

export interface IPicklistUserValues {
  countries: ICountryReadOnlyModel[] | undefined;
  ethnicities: IEthnicityReadOnlyModel[] | undefined;
  genders: IPickListItem[] | undefined;
  languages: IPickListItem[] | undefined;
  races: IRaceReadOnlyModel[] | undefined;
  states: IStateReadOnlyModel[] | undefined;
  statesMap: { [key: string]: IStateReadOnlyModel[] } | undefined;
  defaultStates: IStateReadOnlyModel[] | undefined;
  accreditedInstitutions:
    | IAccreditedProgramInstitutionReadOnlyModel[]
    | undefined;
  trainingTypes: ITrainingTypeReadOnlyModel[] | undefined;
}

export const PICKLISTS_STATE_TOKEN = new StateToken<IPicklist>('picklists');

@State<IPicklist>({
  name: PICKLISTS_STATE_TOKEN,
  defaults: {
    countries: undefined,
    ethnicities: undefined,
    genders: undefined,
    languages: undefined,
    races: undefined,
    states: [],
    statesMap: {},
    defaultStates: undefined,
    accreditedInstitutions: undefined,
    trainingTypes: undefined,
    clinicalLevels: undefined,
    clinicalActivities: undefined,
  },
})
@Injectable()
export class PicklistsState {
  constructor(
    private _store: Store,
    private picklistsService: PicklistsService
  ) {}

  @Action(GetCountryList)
  getCountryList(
    ctx: StateContext<IPicklist>
  ): Observable<ICountryReadOnlyModel[] | undefined> {
    if (ctx.getState()?.countries) {
      return of(ctx.getState()?.countries);
    }
    return this.picklistsService.retrieveCountryReadOnly_GetAll().pipe(
      tap((countries: ICountryReadOnlyModel[]) => {
        ctx.patchState({
          countries,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store', error);
        return of(error);
      })
    );
  }

  @Action(GetEthnicityList)
  getEthnicityList(
    ctx: StateContext<IPicklist>
  ): Observable<IEthnicityReadOnlyModel[] | undefined> {
    if (ctx.getState()?.ethnicities) {
      return of(ctx.getState()?.ethnicities);
    }
    return this.picklistsService.retrieveEthnicityReadOnly_GetAll().pipe(
      tap((ethnicities: IEthnicityReadOnlyModel[]) => {
        ctx.patchState({
          ethnicities,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Ethnicities', error);
        return of(error);
      })
    );
  }

  @Action(GetGenderList)
  getGenderList(
    ctx: StateContext<IPicklist>
  ): Observable<IPickListItem[] | undefined> {
    if (ctx.getState()?.genders) {
      return of(ctx.getState()?.genders);
    }
    return this.picklistsService.retrieveGenderReadOnly_GetAll().pipe(
      tap((genders: IGenderReadOnlyModel[]) => {
        const transGenders = [] as IPickListItem[];
        genders.forEach((gender) => {
          transGenders.push({
            itemValue: gender.itemValue?.toString(),
            itemDescription: gender.itemDescription,
          });
        });

        ctx.patchState({
          genders: transGenders,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Genders', error);
        return of(error);
      })
    );
  }

  @Action(GetLanguageList)
  getLanguageList(
    ctx: StateContext<IPicklist>
  ): Observable<IPickListItem[] | undefined> {
    if (ctx.getState()?.languages) {
      return of(ctx.getState()?.languages);
    }
    return this.picklistsService.retrieveLanguageReadOnly_GetAll().pipe(
      tap((languages: ILanguageReadOnlyModel[]) => {
        const transLanguages = [] as IPickListItem[];
        languages.forEach((language) => {
          transLanguages.push({
            itemValue: language.itemValue?.toString(),
            itemDescription: language.itemDescription,
          });
        });
        ctx.patchState({
          languages: transLanguages,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Languages', error);
        return of(error);
      })
    );
  }

  @Action(GetRaceList)
  getRaceList(
    ctx: StateContext<IPicklist>
  ): Observable<IRaceReadOnlyModel[] | undefined> {
    if (ctx.getState()?.races) {
      return of(ctx.getState()?.races);
    }
    return this.picklistsService.retrieveRaceReadOnly_GetAll().pipe(
      tap((races: IRaceReadOnlyModel[]) => {
        ctx.patchState({
          races,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Races', error);
        return of(error);
      })
    );
  }

  @Action(GetStateList)
  getStateList(
    ctx: StateContext<IPicklist>,
    payload?: { countryCode: string }
  ): Observable<IStateReadOnlyModel[] | undefined> {
    const statesMap = ctx.getState()?.statesMap
      ? ctx.getState()?.statesMap
      : {};
    if (payload?.countryCode && statesMap[payload.countryCode]) {
      const states: IStateReadOnlyModel[] | undefined =
        statesMap[payload.countryCode];
      ctx.patchState({
        states,
      });
      return of(ctx.getState().states);
    }
    if (payload?.countryCode) {
      return this.picklistsService
        .retrieveStateReadOnly_GetByCountry(payload.countryCode)
        .pipe(
          tap((states: IStateReadOnlyModel[]) => {
            const newStatesMap = { ...statesMap };
            newStatesMap[payload.countryCode] = states;
            ctx.patchState({
              states,
              statesMap: newStatesMap,
            });
          }),
          catchError((error) => {
            console.error('------- In Picklists Store: States', error);
            return of(error);
          })
        );
    } else {
      ctx.patchState({
        states: [],
      });
      return of(ctx.getState().states);
    }
  }

  @Action(GetAccreditedProgramInstitutionsList)
  getAccreditedProgramInstitutionsList(
    ctx: StateContext<IPicklist>
  ): Observable<IAccreditedProgramInstitutionReadOnlyModel[] | undefined> {
    if (ctx.getState()?.accreditedInstitutions) {
      return of(ctx.getState()?.accreditedInstitutions);
    }
    return this.picklistsService
      .retrieveAccreditedProgramInstitutionReadOnly_GetAll()
      .pipe(
        tap((insitutions: IAccreditedProgramInstitutionReadOnlyModel[]) => {
          ctx.patchState({
            accreditedInstitutions: insitutions,
          });
        }),
        catchError((error) => {
          console.error(
            '------- In Picklists Store: Accredited Institutions',
            error
          );
          return of(error);
        })
      );
  }

  @Action(GetTrainingTypeList)
  getTrainingTypeList(
    ctx: StateContext<IPicklist>
  ): Observable<ITrainingTypeReadOnlyModel[] | undefined> {
    if (ctx.getState()?.trainingTypes) {
      return of(ctx.getState()?.trainingTypes);
    }
    return this.picklistsService.retrieveTrainingTypeReadOnly_GetAll().pipe(
      tap((trainingTypes: ITrainingTypeReadOnlyModel[]) => {
        ctx.patchState({
          trainingTypes,
        });
      }),
      catchError((error) => {
        console.error(
          '------- In Picklists Store: Accredited Institutions',
          error
        );
        return of(error);
      })
    );
  }

  @Action(GetClinicalLevelList)
  getClinicalLevelList(
    ctx: StateContext<IPicklist>
  ): Observable<IClinicalLevelReadOnlyModel[] | undefined> {
    if (ctx.getState()?.clinicalLevels) {
      return of(ctx.getState()?.clinicalLevels);
    }
    return this.picklistsService.retrieveClinicalLevelReadOnly_GetAll().pipe(
      tap((clinicalLevels: IClinicalLevelReadOnlyModel[]) => {
        ctx.patchState({
          clinicalLevels,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Clinical Levels', error);
        return of(error);
      })
    );
  }

  @Action(GetClinicalActivityList)
  getClinicalActivityList(
    ctx: StateContext<IPicklist>
  ): Observable<IClinicalActivityReadOnlyModel[] | undefined> {
    if (ctx.getState()?.clinicalActivities) {
      return of(ctx.getState()?.clinicalActivities);
    }
    return this.picklistsService.retrieveClinicalActivityReadOnly_GetAll().pipe(
      tap((clinicalActivities: IClinicalActivityReadOnlyModel[]) => {
        ctx.patchState({
          clinicalActivities,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Clinical Activities', error);
        return of(error);
      })
    );
  }

  @Action(GetPicklists)
  getPicklists(
    ctx: StateContext<IPicklist>,
    payload?: { countryCode: string }
  ): Observable<IPicklist> {
    const joins = [
      this.getCountryList(ctx).pipe(catchError((error) => of(error))),
      this.getEthnicityList(ctx).pipe(catchError((error) => of(error))),
      this.getGenderList(ctx).pipe(catchError((error) => of(error))),
      this.getLanguageList(ctx).pipe(catchError((error) => of(error))),
      this.getRaceList(ctx).pipe(catchError((error) => of(error))),
      this.getAccreditedProgramInstitutionsList(ctx).pipe(
        catchError((error) => of(error))
      ),
      this.getTrainingTypeList(ctx).pipe(catchError((error) => of(error))),
    ];

    if (payload?.countryCode) {
      joins.push(
        this.getStateList(ctx, { countryCode: payload.countryCode }).pipe(
          catchError((error) => of(error))
        )
      );
    }

    return forkJoin(joins).pipe(
      map((picklists: IPicklist[]) => {
        return of(ctx.getState());
      }),
      share(),
      catchError((error) => {
        console.error('------- In Picklists Store', error);
        return of(error);
      })
    );
  }
}
