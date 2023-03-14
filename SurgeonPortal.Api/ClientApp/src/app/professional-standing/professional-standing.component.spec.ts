import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfessionalStandingComponent } from './professional-standing.component';

describe('ProfessionalStandingComponent', () => {
  let component: ProfessionalStandingComponent;
  let fixture: ComponentFixture<ProfessionalStandingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfessionalStandingComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ProfessionalStandingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
