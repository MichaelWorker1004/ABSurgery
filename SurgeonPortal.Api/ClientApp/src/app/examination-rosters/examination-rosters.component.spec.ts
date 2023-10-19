import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationRostersComponent } from './examination-rosters.component';
import { NgxsModule } from '@ngxs/store';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { TranslateModule } from '@ngx-translate/core';

describe('ExaminationRostersComponent', () => {
  let component: ExaminationRostersComponent;
  let fixture: ComponentFixture<ExaminationRostersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ExaminationRostersComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
        TranslateModule.forRoot(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ExaminationRostersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
