import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAccountComponent } from './my-account.component';
import { HttpClientModule } from '@angular/common/http';
import { NgxsModule, Store } from '@ngxs/store';

import { surgeonPortalState } from '../state/surgeon-portal.state';
import { UserAccountService } from '../../../mock-server/user-account.service';

describe('MyAccountComponent', () => {
  let component: MyAccountComponent;
  let fixture: ComponentFixture<MyAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
        NgxsModule.forRoot(surgeonPortalState),
        MyAccountComponent,
      ],
      providers: [UserAccountService, Store],
    }).compileComponents();

    fixture = TestBed.createComponent(MyAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
