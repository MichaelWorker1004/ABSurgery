import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAccountComponent } from './my-account.component';
import { HttpClientModule } from '@angular/common/http';
import { NgxsModule } from '@ngxs/store';
import { AuthState } from '../state/auth/ngxs-auth.state';

describe('MyAccountComponent', () => {
  let component: MyAccountComponent;
  let fixture: ComponentFixture<MyAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
        NgxsModule.forRoot([AuthState]),
        MyAccountComponent,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(MyAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
