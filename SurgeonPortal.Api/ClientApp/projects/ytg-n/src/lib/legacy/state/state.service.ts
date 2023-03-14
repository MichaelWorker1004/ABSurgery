import { Injectable } from '@angular/core';

import { BehaviorSubject, Subscription } from 'rxjs';

export type StateChange<T> = {
  key: string;
  value: T;
  previous: T;
};

export class InnerStateService {
  valueChanged = new BehaviorSubject<StateChange<any>>(null);

  private values: { [key: string]: any } = {};

  public getValue<T>(key: string): T {
    return this.values[key];
  }

  public setValue(key: string, value: any) {
    let previous = this.values[key];
    this.values[key] = value;
    this.valueChanged.next({
      key: key,
      value: value,
      previous: previous,
    });
  }
}

export const AppStateService = new InnerStateService();

@Injectable()
export class StateService {
  public subscribe<T>(
    key: string,
    callback: (change: StateChange<T>) => any
  ): Subscription {
    return AppStateService.valueChanged.subscribe((change) => {
      if (change.key === key) {
        callback(change);
      }
    });
  }
}
