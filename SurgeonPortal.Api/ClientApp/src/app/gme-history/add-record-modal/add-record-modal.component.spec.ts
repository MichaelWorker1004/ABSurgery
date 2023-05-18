import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRecordModalComponent } from './add-record-modal.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule } from '@ngxs/store';
import { surgeonPortalState } from '../../state/surgeon-portal.state';

describe('AddRecordModalComponent', () => {
  let component: AddRecordModalComponent;
  let fixture: ComponentFixture<AddRecordModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxsModule.forRoot(surgeonPortalState),
        AddRecordModalComponent,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(AddRecordModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
