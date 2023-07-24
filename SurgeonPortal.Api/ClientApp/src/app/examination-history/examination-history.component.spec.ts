import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationHistoryComponent } from './examination-history.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';

describe('ExaminationHistoryComponent', () => {
  let component: ExaminationHistoryComponent;
  let fixture: ComponentFixture<ExaminationHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ExaminationHistoryComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ExaminationHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
