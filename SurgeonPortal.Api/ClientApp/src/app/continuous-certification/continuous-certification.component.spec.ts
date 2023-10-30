import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';

import { ContinuousCertificationComponent } from './continuous-certification.component';
import { TranslateModule } from '@ngx-translate/core';

describe('ContinuousCertificationComponent', () => {
  let component: ContinuousCertificationComponent;
  let fixture: ComponentFixture<ContinuousCertificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        ContinuousCertificationComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
        TranslateModule.forRoot(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ContinuousCertificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
