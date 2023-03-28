import { importProvidersFrom } from '@angular/core';
import { AppComponent } from './app/app.component';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { NgxsStoragePluginModule, StorageOption } from '@ngxs/storage-plugin';
import { NgxsModule } from '@ngxs/store';
import { AppRoutingModule } from './app/app-routing.module';
import {
  withInterceptorsFromDi,
  provideHttpClient,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { surgeonPortalState } from './app/state/surgeon-portal.state';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { IConfig, provideEnvironmentNgxMask } from 'ngx-mask';
import { AuthInterceptor } from './app/shared/interceptors/auth.interceptor';

// TODO: Explore ngx-mask configs to see if onkeypress
//  and onchange can be mapped to sl_change, etc
// This is for the ngx-mask library
const maskConfig: Partial<IConfig> = {
  validation: false,
};
bootstrapApplication(AppComponent, {
  providers: [
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
        key: ['auth'],
      }),
      NgxsLoggerPluginModule.forRoot(),
      NgxsReduxDevtoolsPluginModule.forRoot(),
      NgxsFormPluginModule.forRoot()
    ),
    provideHttpClient(withInterceptorsFromDi()),
  ],
}).catch((err) => console.error(err));
