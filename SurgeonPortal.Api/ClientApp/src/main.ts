import { importProvidersFrom } from '@angular/core';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import {
  withInterceptorsFromDi,
  provideHttpClient,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';

import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import {
  LOCAL_STORAGE_ENGINE,
  NgxsStoragePluginModule,
  SESSION_STORAGE_ENGINE,
  StorageOption,
} from '@ngxs/storage-plugin';
import { NgxsModule } from '@ngxs/store';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { IConfig, provideEnvironmentNgxMask } from 'ngx-mask';

import { AuthInterceptor } from './app/shared/common';
import { ErrorInterceptor } from './app/shared/common';
import { surgeonPortalState } from './app/state/surgeon-portal.state';

import { AppRoutingModule } from './app/app-routing.module';
import { AppComponent } from './app/app.component';
import { AUTH_STATE_TOKEN, USER_PROFILE_STATE_TOKEN } from './app/state';
import { PICKLISTS_STATE_TOKEN } from './app/state/picklists';
import {
  BrowserAnimationsModule,
  provideAnimations,
} from '@angular/platform-browser/animations';

// TODO: Explore ngx-mask configs to see if onkeypress
//  and onchange can be mapped to sl_change, etc
// This is for the ngx-mask library
const maskConfig: Partial<IConfig> = {
  validation: false,
};
bootstrapApplication(AppComponent, {
  providers: [
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
        key: [
          AUTH_STATE_TOKEN,
          USER_PROFILE_STATE_TOKEN,
          PICKLISTS_STATE_TOKEN,
        ],
      }),
      // NgxsStoragePluginModule.forRoot({
      //   key: [
      //     {
      //       key: AUTH_STATE_TOKEN,
      //       engine: SESSION_STORAGE_ENGINE,
      //     },
      //     {
      //       key: USER_PROFILE_STATE_TOKEN,
      //       engine: SESSION_STORAGE_ENGINE,
      //     },
      //     {
      //       key: PICKLISTS_STATE_TOKEN,
      //       engine: SESSION_STORAGE_ENGINE,
      //     },
      //   ],
      // }),
      // NgxsLoggerPluginModule.forRoot(),
      NgxsReduxDevtoolsPluginModule.forRoot(),
      NgxsFormPluginModule.forRoot()
    ),
    provideHttpClient(withInterceptorsFromDi()),
    provideAnimations(),
  ],
}).catch((err) => console.error(err));
