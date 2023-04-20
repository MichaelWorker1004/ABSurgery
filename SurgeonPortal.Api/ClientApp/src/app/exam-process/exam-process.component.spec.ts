import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExamProcessComponent } from './exam-process.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('ExamProcessComponent', () => {
  let component: ExamProcessComponent;
  let fixture: ComponentFixture<ExamProcessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExamProcessComponent, RouterTestingModule],
    }).compileComponents();

    fixture = TestBed.createComponent(ExamProcessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
