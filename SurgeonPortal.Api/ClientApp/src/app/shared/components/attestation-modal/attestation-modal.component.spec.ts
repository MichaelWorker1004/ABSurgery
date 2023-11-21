import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttestationModalComponent } from './attestation-modal.component';

describe('AttestationModalComponent', () => {
  let component: AttestationModalComponent;
  let fixture: ComponentFixture<AttestationModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AttestationModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AttestationModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
