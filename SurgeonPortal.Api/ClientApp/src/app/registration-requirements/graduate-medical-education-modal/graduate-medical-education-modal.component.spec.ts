import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GraduateMedicalEducationModalComponent } from './graduate-medical-education-modal.component';

describe('GraduateMedicalEducationModalComponent', () => {
  let component: GraduateMedicalEducationModalComponent;
  let fixture: ComponentFixture<GraduateMedicalEducationModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GraduateMedicalEducationModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(GraduateMedicalEducationModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
