import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationScoreModalComponent } from './examination-score-modal.component';

describe('ExaminationScoreModalComponent', () => {
  let component: ExaminationScoreModalComponent;
  let fixture: ComponentFixture<ExaminationScoreModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExaminationScoreModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ExaminationScoreModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
