import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { ApiService } from './api.service';

import { StateService } from '../state';

import { ConnectService } from './connect';
import { AccountService } from './account';

@NgModule({
  imports: [CommonModule, HttpClientModule],
  providers: [ApiService, StateService, ConnectService, AccountService],
})
export class ApiModule {}
