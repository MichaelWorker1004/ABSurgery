import { bootstrapApplication } from '@angular/platform-browser';
import { DEFAULT_PROVIDERS } from './default-providers';
import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, {
  providers: [...DEFAULT_PROVIDERS],
}).catch((err) => console.error(err));
