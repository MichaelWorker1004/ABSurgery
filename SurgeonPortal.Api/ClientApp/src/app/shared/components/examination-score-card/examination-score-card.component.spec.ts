import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationScoreCardComponent } from './examination-score-card.component';
import { TranslateModule } from '@ngx-translate/core';

describe('ExaminationScoreCardComponent', () => {
  let component: ExaminationScoreCardComponent;
  let fixture: ComponentFixture<ExaminationScoreCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExaminationScoreCardComponent, TranslateModule.forRoot()],
    }).compileComponents();

    fixture = TestBed.createComponent(ExaminationScoreCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
