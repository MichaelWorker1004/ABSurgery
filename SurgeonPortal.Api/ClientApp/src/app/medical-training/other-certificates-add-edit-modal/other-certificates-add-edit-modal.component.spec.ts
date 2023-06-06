import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OtherCertificatesAddEditModalComponent } from './other-certificates-add-edit-modal.component';
import { NgxsModule } from '@ngxs/store';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';

describe('OtherCertificatesAddEditModalComponent', () => {
  let component: OtherCertificatesAddEditModalComponent;
  let fixture: ComponentFixture<OtherCertificatesAddEditModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        OtherCertificatesAddEditModalComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(OtherCertificatesAddEditModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
