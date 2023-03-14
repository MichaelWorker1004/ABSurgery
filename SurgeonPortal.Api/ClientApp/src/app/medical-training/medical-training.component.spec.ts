import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalTrainingComponent } from './medical-training.component';

describe('MedicalTrainingComponent', () => {
  let component: MedicalTrainingComponent;
  let fixture: ComponentFixture<MedicalTrainingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedicalTrainingComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MedicalTrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
