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
  PicklistsService,
} from '../../api';
import {
  GetCountryList,
  GetEthnicityList,
  GetGenderList,
  GetLanguageList,
  GetPicklists,
  GetRaceList,
  GetStateList,
} from './picklists.actions';
import { IFormErrors } from '../../shared/common';
export interface IPicklist {
  countries: ICountryReadOnlyModel[] | undefined;
  ethnicities: IEthnicityReadOnlyModel[] | undefined;
  genders: IGenderReadOnlyModel[] | undefined;
  languages: ILanguageReadOnlyModel[] | undefined;
  races: IRaceReadOnlyModel[] | undefined;
  states: IStateReadOnlyModel[] | undefined;
  statesMap: { [key: string]: IStateReadOnlyModel[] };
  errors?: IFormErrors | undefined;
}

export interface IPickListItem {
  itemValue: string | number | null | undefined;
  itemDescription: string | null | undefined;
}

export interface IPicklistUserValues {
  countries: ICountryReadOnlyModel[] | undefined;
  ethnicities: IEthnicityReadOnlyModel[] | undefined;
  genders: IGenderReadOnlyModel[] | undefined;
  languages: ILanguageReadOnlyModel[] | undefined;
  races: IRaceReadOnlyModel[] | undefined;
  states: IStateReadOnlyModel[] | undefined;
  statesMap: { [key: string]: IStateReadOnlyModel[] } | undefined;
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
  ): Observable<IGenderReadOnlyModel[] | undefined> {
    if (ctx.getState()?.genders) {
      return of(ctx.getState()?.genders);
    }
    return this.picklistsService.retrieveGenderReadOnly_GetAll().pipe(
      tap((genders: IGenderReadOnlyModel[]) => {
        ctx.patchState({
          genders,
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
  ): Observable<ILanguageReadOnlyModel[] | undefined> {
    if (ctx.getState()?.languages) {
      return of(ctx.getState()?.languages);
    }
    return this.picklistsService.retrieveLanguageReadOnly_GetAll().pipe(
      tap((languages: ILanguageReadOnlyModel[]) => {
        ctx.patchState({
          languages,
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
    if (payload && payload.countryCode) {
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
      return of(ctx.getState().states);
    }
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
    ];

    if (payload && payload.countryCode) {
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
