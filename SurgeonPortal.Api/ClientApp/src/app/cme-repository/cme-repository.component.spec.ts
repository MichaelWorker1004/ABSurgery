import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CmeRepositoryComponent } from './cme-repository.component';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { NgxsModule } from '@ngxs/store';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CmeRepositoryComponent', () => {
  let component: CmeRepositoryComponent;
  let fixture: ComponentFixture<CmeRepositoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        CmeRepositoryComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(CmeRepositoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
