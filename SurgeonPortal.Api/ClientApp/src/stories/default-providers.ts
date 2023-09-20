import {
  HTTP_INTERCEPTORS,
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { NgxsStoragePluginModule, StorageOption } from '@ngxs/storage-plugin';
import { NgxsModule } from '@ngxs/store';
import { IConfig, provideEnvironmentNgxMask } from 'ngx-mask';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { ErrorInterceptor, AuthInterceptor } from 'src/app/shared/common';
import { AUTH_STATE_TOKEN, USER_PROFILE_STATE_TOKEN } from 'src/app/state';
import { PICKLISTS_STATE_TOKEN } from 'src/app/state/picklists';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';

const maskConfig: Partial<IConfig> = {
  validation: false,
};

export const DEFAULT_PROVIDERS = [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  provideEnvironmentNgxMask(maskConfig),
  importProvidersFrom(
    BrowserModule,
    AppRoutingModule,
    NgxsModule.forRoot(surgeonPortalState, {
      developmentMode: true,
    }),
    NgxsStoragePluginModule.forRoot({
      storage: StorageOption.SessionStorage,
      key: [AUTH_STATE_TOKEN, USER_PROFILE_STATE_TOKEN, PICKLISTS_STATE_TOKEN],
    }),
    NgxsLoggerPluginModule.forRoot(),
    NgxsReduxDevtoolsPluginModule.forRoot(),
    NgxsFormPluginModule.forRoot()
  ),
  provideHttpClient(withInterceptorsFromDi()),
  provideAnimations(),
];
