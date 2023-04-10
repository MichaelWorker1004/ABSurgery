import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfessionalActivitiesAndPrivilegesModalComponent } from './professional-activities-and-privileges-modal.component';

describe('ProfessionalActivitiesAndPrivilegesModalComponent', () => {
  let component: ProfessionalActivitiesAndPrivilegesModalComponent;
  let fixture: ComponentFixture<ProfessionalActivitiesAndPrivilegesModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfessionalActivitiesAndPrivilegesModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(
      ProfessionalActivitiesAndPrivilegesModalComponent
    );
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
