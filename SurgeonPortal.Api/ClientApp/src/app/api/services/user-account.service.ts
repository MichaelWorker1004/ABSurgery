import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { IUserAccountModel } from '../models/user-account.model';

export interface IUserResponse {
  data: { userAccount: IUserAccountModel };
}

@Injectable({
  providedIn: 'root',
})
export class UserAccountService {
  constructor(private httpClient: HttpClient) {}

  getUser(): Observable<IUserAccountModel> {
    return this.httpClient
      .get<IUserResponse>('http://localhost:3000/userAccount')
      .pipe(
        map((response: IUserResponse) => {
          console.log(response.data.userAccount);
          return response.data.userAccount;
        })
      );
  }

  setUser(payload: IUserAccountModel): Observable<IUserAccountModel> {
    return this.httpClient
      .post<IUserResponse>('http://localhost:3000/userAccount', {
        body: payload,
      })
      .pipe(
        map((response: IUserResponse) => {
          return response.data.userAccount as IUserAccountModel;
        })
      );
  }
}
