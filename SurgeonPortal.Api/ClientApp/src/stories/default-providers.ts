import {
  HTTP_INTERCEPTORS,
  provideHttpClient,
  withInterceptorsFromDi,
  HttpClient,
} from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { NgxsStoragePluginModule, StorageOption } from '@ngxs/storage-plugin';
import { NgxsModule } from '@ngxs/store';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { IConfig, provideEnvironmentNgxMask } from 'ngx-mask';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { ErrorInterceptor, AuthInterceptor } from 'src/app/shared/common';
import { AUTH_STATE_TOKEN, USER_PROFILE_STATE_TOKEN } from 'src/app/state';
import { PICKLISTS_STATE_TOKEN } from 'src/app/state/picklists';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';

import { environment } from 'src/environments/environment';

const maskConfig: Partial<IConfig> = {
  validation: false,
};

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

export const DEFAULT_PROVIDERS = [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  provideEnvironmentNgxMask(maskConfig),
  importProvidersFrom(
    BrowserModule,
    AppRoutingModule,
    TranslateModule.forRoot({
      defaultLanguage: 'en-US',
      loader: {
        provide: TranslateLoader,
        useFactory: createTranslateLoader,
        deps: [HttpClient],
      },
    }),
    NgxsModule.forRoot(surgeonPortalState, {
      developmentMode: environment.production,
    }),
    NgxsStoragePluginModule.forRoot({
      storage: StorageOption.SessionStorage,
      key: [AUTH_STATE_TOKEN, USER_PROFILE_STATE_TOKEN, PICKLISTS_STATE_TOKEN],
    }),
    NgxsLoggerPluginModule.forRoot({ disabled: !environment.production }),
    NgxsReduxDevtoolsPluginModule.forRoot({
      disabled: !environment.production,
    }),
    NgxsFormPluginModule.forRoot()
  ),
  provideHttpClient(withInterceptorsFromDi()),
  provideAnimations(),
];
