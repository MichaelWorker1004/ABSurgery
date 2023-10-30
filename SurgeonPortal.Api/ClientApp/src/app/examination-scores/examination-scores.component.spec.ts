import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationScoresComponent } from './examination-scores.component';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TranslateModule } from '@ngx-translate/core';

describe('ExaminationScoresComponent', () => {
  let component: ExaminationScoresComponent;
  let fixture: ComponentFixture<ExaminationScoresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ExaminationScoresComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
        TranslateModule.forRoot(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ExaminationScoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
