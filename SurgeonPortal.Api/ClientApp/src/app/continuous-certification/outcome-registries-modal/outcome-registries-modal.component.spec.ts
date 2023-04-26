import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OutcomeRegistriesModalComponent } from './outcome-registries-modal.component';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('OutcomeRegistriesModalComponent', () => {
  let component: OutcomeRegistriesModalComponent;
  let fixture: ComponentFixture<OutcomeRegistriesModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        OutcomeRegistriesModalComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(OutcomeRegistriesModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
