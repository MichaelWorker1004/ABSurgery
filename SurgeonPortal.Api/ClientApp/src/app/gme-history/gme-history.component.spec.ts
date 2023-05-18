import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GmeHistoryComponent } from './gme-history.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';

describe('GmeHistoryComponent', () => {
  let component: GmeHistoryComponent;
  let fixture: ComponentFixture<GmeHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxsModule.forRoot(surgeonPortalState),
        GmeHistoryComponent,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(GmeHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
