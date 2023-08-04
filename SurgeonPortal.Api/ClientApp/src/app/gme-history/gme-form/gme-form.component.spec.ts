import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GmeFormComponent } from './gme-form.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';

describe('GmeFormComponent', () => {
  let component: GmeFormComponent;
  let fixture: ComponentFixture<GmeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxsModule.forRoot(surgeonPortalState),
        GmeFormComponent,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(GmeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
