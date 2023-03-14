import { Observable } from 'rxjs';

export class ResponseObservable<T> extends Observable<T> {}
export class PagedResponseObservable<T> extends Observable<PagedData<T>> {}

export class ApiConfig {
  constructor(
    public readonly url: string,
    public readonly throwErrors?: boolean
  ) {}
}

export class PagedData<T> {
  public pageNumber: number;
  public pageSize: number;
  public totalResults: number;
  public totalPages: number;
  public results: T[];
}

export class ErrorMessage {
  public field: string;
  public errors: string[];
}
