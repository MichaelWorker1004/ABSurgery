import { importProvidersFrom } from '@angular/core';
import { AppComponent } from './app/app.component';
import { NgxsRouterPluginModule } from '@ngxs/router-plugin';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { NgxsStoragePluginModule, StorageOption } from '@ngxs/storage-plugin';
import { MyAccountState } from './app/state/my-account/my-account.state';
import { SimpleValueState } from './app/state/simple-value.state';
import { NgxsModule } from '@ngxs/store';
import { AppRoutingModule } from './app/app-routing.module';
import {
  withInterceptorsFromDi,
  provideHttpClient,
} from '@angular/common/http';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(
      BrowserModule,
      AppRoutingModule,
      NgxsModule.forRoot([SimpleValueState, MyAccountState], {
        developmentMode: true,
      }),
      NgxsStoragePluginModule.forRoot({
        storage: StorageOption.SessionStorage,
        key: 'auth.token',
      }),
      NgxsLoggerPluginModule.forRoot(),
      NgxsReduxDevtoolsPluginModule.forRoot(),
      NgxsFormPluginModule.forRoot(),
      NgxsRouterPluginModule.forRoot()
    ),
    provideHttpClient(withInterceptorsFromDi()),
  ],
}).catch((err) => console.error(err));
