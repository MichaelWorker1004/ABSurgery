import { Injectable } from '@angular/core';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import { LoadApplication } from './application.actions';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

export interface IApplicationState {
  isLoggedIn: boolean;
  isLoaded: boolean;
  isAuth: boolean;
  isUserLoaded: boolean;
  featureFlags: IFeatureFlags;
}

export interface IFeatureFlags {
  ceScoreTesting?: boolean;
}

export const APPLICATION_STATE_TOKEN = new StateToken<IApplicationState>(
  'application'
);

@State<IApplicationState>({
  name: APPLICATION_STATE_TOKEN,
  defaults: {
    isLoggedIn: false,
    isLoaded: false,
    isAuth: false,
    isUserLoaded: false,
    featureFlags: {},
  },
})
@Injectable({ providedIn: 'root' })
export class ApplicationState {
  constructor(
    private store: Store,
    private router: Router,
    private httpClient: HttpClient
  ) {}

  @Action(LoadApplication)
  loadApplication(
    ctx: StateContext<IApplicationState>
  ): Observable<IFeatureFlags> {
    const state = ctx.getState();
    return this.getFeatureFlags().pipe(
      tap((response: any) => {
        ctx.patchState({
          featureFlags: response as IFeatureFlags,
        });
      })
    );
  }

  private getFeatureFlags(): Observable<IFeatureFlags> {
    return this.httpClient.get('/api/features').pipe(
      map((response: any) => {
        return response as IFeatureFlags;
      })
    );
  }
}
