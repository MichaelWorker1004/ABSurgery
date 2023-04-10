import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OutcomeRegistriesModalComponent } from './outcome-registries-modal.component';

describe('OutcomeRegistriesModalComponent', () => {
  let component: OutcomeRegistriesModalComponent;
  let fixture: ComponentFixture<OutcomeRegistriesModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OutcomeRegistriesModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(OutcomeRegistriesModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
