import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class YtgAngularService {
  constructor(private httpClient: HttpClient) {}
}
