import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ApiService extends HttpClient {
  constructor(private httpHandler: HttpHandler) {
    super(httpHandler);
  }
}
