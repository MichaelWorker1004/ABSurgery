import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationScoreModalComponent } from './examination-score-modal.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';

describe('ExaminationScoreModalComponent', () => {
  let component: ExaminationScoreModalComponent;
  let fixture: ComponentFixture<ExaminationScoreModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ExaminationScoreModalComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ExaminationScoreModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
