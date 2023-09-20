import { DEFAULT_PROVIDERS } from './stories/default-providers';
import { bootstrapApplication } from '@angular/platform-browser';

import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, {
  providers: [...DEFAULT_PROVIDERS],
}).catch((err) => console.error(err));
