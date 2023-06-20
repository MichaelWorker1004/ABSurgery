import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationScoresComponent } from './examination-scores.component';

describe('ExaminationScoresComponent', () => {
  let component: ExaminationScoresComponent;
  let fixture: ComponentFixture<ExaminationScoresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExaminationScoresComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ExaminationScoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
