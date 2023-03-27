import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseAddEditModalComponent } from './license-add-edit-modal.component';

describe('LicenseAddEditModalComponent', () => {
  let component: LicenseAddEditModalComponent;
  let fixture: ComponentFixture<LicenseAddEditModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LicenseAddEditModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(LicenseAddEditModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
