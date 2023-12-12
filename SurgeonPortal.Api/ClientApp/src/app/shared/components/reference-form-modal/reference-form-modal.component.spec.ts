import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferenceFormModalComponent } from './reference-form-modal.component';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ReferenceFormModalComponent', () => {
  let component: ReferenceFormModalComponent;
  let fixture: ComponentFixture<ReferenceFormModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ReferenceFormModalComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ReferenceFormModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
