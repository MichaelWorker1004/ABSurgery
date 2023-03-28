import { TestBed } from '@angular/core/testing';

import { UserCredentialsApiService } from './user-credentials.service';
import { HttpClientModule } from '@angular/common/http';

describe('UserCredentialsApiService', () => {
  let service: UserCredentialsApiService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [],
    });
    service = TestBed.inject(UserCredentialsApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
