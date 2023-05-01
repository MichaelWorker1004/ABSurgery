import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from '../../../state/surgeon-portal.state';

import { TrainingAddEditModalComponent } from './training-add-edit-modal.component';

describe('TrainingAddEditModalComponent', () => {
  let component: TrainingAddEditModalComponent;
  let fixture: ComponentFixture<TrainingAddEditModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        TrainingAddEditModalComponent,
        NgxsModule.forRoot(surgeonPortalState),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(TrainingAddEditModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
