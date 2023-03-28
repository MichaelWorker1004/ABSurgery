import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { YtgHelperService } from './helper.service';

@NgModule({
  imports: [CommonModule],
  exports: [],
  providers: [YtgHelperService],
})
export class SharedYtgModule {}
