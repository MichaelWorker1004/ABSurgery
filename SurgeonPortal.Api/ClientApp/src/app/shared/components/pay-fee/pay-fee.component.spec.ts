import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayFeeComponent } from './pay-fee.component';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('PayFeeComponent', () => {
  let component: PayFeeComponent;
  let fixture: ComponentFixture<PayFeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        PayFeeComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(PayFeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
