import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgramDirectorAttestationsComponent } from './program-director-attestations.component';

describe('ProgramDirectorAttestationsComponent', () => {
  let component: ProgramDirectorAttestationsComponent;
  let fixture: ComponentFixture<ProgramDirectorAttestationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProgramDirectorAttestationsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ProgramDirectorAttestationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
