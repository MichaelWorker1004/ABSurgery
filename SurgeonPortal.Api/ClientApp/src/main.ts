import { importProvidersFrom } from '@angular/core';
import { AppComponent } from './app/app.component';
import { NgxsRouterPluginModule } from '@ngxs/router-plugin';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
// import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
// import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { NgxsStoragePluginModule, StorageOption } from '@ngxs/storage-plugin';
import { NgxsModule } from '@ngxs/store';
import { AppRoutingModule } from './app/app-routing.module';
import {
  withInterceptorsFromDi,
  provideHttpClient,
} from '@angular/common/http';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { surgeonPortalState } from './app/state/surgeon-portal.state';

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(
      BrowserModule,
      AppRoutingModule,
      NgxsModule.forRoot(surgeonPortalState, {
        developmentMode: true,
      }),
      NgxsStoragePluginModule.forRoot({
        storage: StorageOption.SessionStorage,
        key: 'auth.access_token',
      }),
      // NgxsLoggerPluginModule.forRoot(),
      // NgxsReduxDevtoolsPluginModule.forRoot(),
      NgxsFormPluginModule.forRoot(),
      NgxsRouterPluginModule.forRoot()
    ),
    provideHttpClient(withInterceptorsFromDi()),
  ],
}).catch((err) => console.error(err));
