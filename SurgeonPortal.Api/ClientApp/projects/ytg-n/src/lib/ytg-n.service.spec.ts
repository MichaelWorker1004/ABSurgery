import { TestBed } from '@angular/core/testing';

import { YtgNService } from './ytg-n.service';

describe('YtgNService', () => {
  let service: YtgNService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(YtgNService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
