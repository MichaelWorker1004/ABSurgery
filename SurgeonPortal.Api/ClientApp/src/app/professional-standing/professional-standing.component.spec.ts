import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfessionalStandingComponent } from './professional-standing.component';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ProfessionalStandingComponent', () => {
  let component: ProfessionalStandingComponent;
  let fixture: ComponentFixture<ProfessionalStandingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ProfessionalStandingComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ProfessionalStandingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
