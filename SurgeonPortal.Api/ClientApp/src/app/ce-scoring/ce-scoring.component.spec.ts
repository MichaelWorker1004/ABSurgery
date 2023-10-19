import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CeScoringAppComponent } from './ce-scoring.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { TranslateModule } from '@ngx-translate/core';

describe('CeScoringAppComponent', () => {
  let component: CeScoringAppComponent;
  let fixture: ComponentFixture<CeScoringAppComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        CeScoringAppComponent,
        RouterTestingModule,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
        TranslateModule.forRoot(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(CeScoringAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
