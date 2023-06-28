import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationScoreCardComponent } from './examination-score-card.component';

describe('ExaminationScoreCardComponent', () => {
  let component: ExaminationScoreCardComponent;
  let fixture: ComponentFixture<ExaminationScoreCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExaminationScoreCardComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ExaminationScoreCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
