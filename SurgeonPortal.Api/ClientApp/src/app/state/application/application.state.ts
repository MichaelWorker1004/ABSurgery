import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import { LoadApplication } from './application.actions';

export interface IApplicationState {
  isLoggedIn: boolean;
  isLoaded: boolean;
  isAuth: boolean;
  isUserLoaded: boolean;
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
  },
})
@Injectable({ providedIn: 'root' })
export class ApplicationState {
  constructor(private store: Store) {}

  @Action(LoadApplication)
  loadApplication(ctx: StateContext<IApplicationState>) {
    const state = ctx.getState();
    ctx.setState({
      ...state,
      isLoaded: true,
    });
  }
}
