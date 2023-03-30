import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { RegistrationRequirementsComponent } from './registration-requirements.component';

describe('RegistrationRequirementsComponent', () => {
  let component: RegistrationRequirementsComponent;
  let fixture: ComponentFixture<RegistrationRequirementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, RegistrationRequirementsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RegistrationRequirementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
