import { NgModule } from "@angular/core";

import { VzCommonApiConfig, VzCommonApiModule, VzCommonAuthConfig } from "verizon-angular";
import { ApiConfig, ApiModule } from "ytg-angular";

import { environment } from "src/environments/environment";

@NgModule({
    imports: [
        ApiModule,
        VzCommonApiModule.forRoot(),
    ],
    providers: [
      {
        provide: ApiConfig,
        useValue: new ApiConfig(environment.apiBaseUrl, true)
      },
      {
        provide: VzCommonApiConfig,
        useValue: new VzCommonApiConfig(environment.vzCommonApiBaseUrl, true)
      },
      {
        provide: VzCommonAuthConfig,
        useValue: new VzCommonAuthConfig(environment.vzCommonAuthConfig, 'ODN Tracker', 'odnTracker')
      }
    ]
})
export class ODNTrackerApiModule { }
