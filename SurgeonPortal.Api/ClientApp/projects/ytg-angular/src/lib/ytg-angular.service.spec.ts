import { TestBed } from '@angular/core/testing';

import { YtgAngularService } from './ytg-angular.service';
import { HttpClientModule } from '@angular/common/http';

describe('YtgAngularService', () => {
  let service: YtgAngularService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [],
    });
    service = TestBed.inject(YtgAngularService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
