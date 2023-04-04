import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalLicenseModalComponent } from './medical-license-modal.component';

describe('MedicalLicenseModalComponent', () => {
  let component: MedicalLicenseModalComponent;
  let fixture: ComponentFixture<MedicalLicenseModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedicalLicenseModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MedicalLicenseModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
