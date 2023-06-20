import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationRostersComponent } from './examination-rosters.component';

describe('ExaminationRostersComponent', () => {
  let component: ExaminationRostersComponent;
  let fixture: ComponentFixture<ExaminationRostersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExaminationRostersComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ExaminationRostersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
