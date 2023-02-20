import { NgModule } from '@angular/core';
import { VzCommonAuthViewsModule } from 'verizon-angular';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    VzCommonAuthViewsModule,
    SharedModule
  ]
})
export class AuthModule {}