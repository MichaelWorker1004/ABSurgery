import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SanctionsFormComponent } from './sanctions-form.component';

describe('SanctionsFormComponent', () => {
  let component: SanctionsFormComponent;
  let fixture: ComponentFixture<SanctionsFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SanctionsFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SanctionsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
