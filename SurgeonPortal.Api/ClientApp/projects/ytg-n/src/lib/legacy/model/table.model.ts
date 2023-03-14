import { EventEmitter } from '@angular/core';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';

export class PageableObservable<T> {
  protected _observable: BehaviorSubject<T[]> = new BehaviorSubject(null);
  protected _trigger = new EventEmitter<triggerPageParams>();

  public get observable() {
    return this._observable;
  }

  constructor(
    public provide: (
      page: number,
      pageSize: number
    ) => Observable<PageableData<T>>
  ) {}

  public trigger(pageNumber?: number, pageSize?: number) {
    this._trigger.emit({
      pageNumber: pageNumber,
      pageSize: pageSize,
    });
  }

  public onTrigger(
    callback: (params?: triggerPageParams) => any
  ): Subscription {
    return this._trigger.subscribe(callback);
  }
}

export interface PageableData<T> {
  pageNumber: number;
  pageSize: number;
  totalResults: number;
  totalPages: number;
  results: T[];
}

export interface TableProgressOptions {
  diameter?: number;
  color?: string;
}

export interface TableCellData {
  columnIndex: number;
  rowIndex: number;
  data: any;
  row: any[];
}

export interface TableCellRoute {
  externalLink?: boolean;
  route: string;
  queryParams?: any;
  queryParamsHandling?: string;
  skipLocationChange?: boolean;
}

export interface TableOnSortEvent {
  active: string;
  direction: 'asc' | 'desc' | '';
}

export type triggerPageParams = {
  pageNumber: number;
  pageSize: number;
};

export enum TableColumnType {
  ReadonlyText = 0,
  Checkbox = 1,
  Date = 2,
  CustomHTML = 3,
  DateType = 4,
  CustomButton = 5,
  CellImage = 6,
}
