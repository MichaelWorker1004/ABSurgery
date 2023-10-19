import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OralExaminationsComponent } from './oral-examinations.component';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TranslateModule } from '@ngx-translate/core';

describe('OralExaminationsComponent', () => {
  let component: OralExaminationsComponent;
  let fixture: ComponentFixture<OralExaminationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        OralExaminationsComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
        TranslateModule.forRoot(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(OralExaminationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
