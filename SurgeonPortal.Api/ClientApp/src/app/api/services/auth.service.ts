import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

export interface User {
  token: string | null;
  username: string | null;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private httpClient: HttpClient) {}

  login(payload: any): Observable<{ token: string }> {
    console.log('payload:', payload);
    return of({ token: 'asdfasdfasdfasdfasdf' });
  }

  logout(token: any): Observable<any> {
    return of('logged out');
  }
}
