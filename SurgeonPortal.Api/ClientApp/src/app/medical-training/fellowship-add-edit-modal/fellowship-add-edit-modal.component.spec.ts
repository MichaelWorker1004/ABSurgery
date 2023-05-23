import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FellowshipAddEditModalComponent } from './fellowship-add-edit-modal.component';
import { surgeonPortalState } from 'src/app/state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxsModule } from '@ngxs/store';

describe('FellowshipAddEditModalComponent', () => {
  let component: FellowshipAddEditModalComponent;
  let fixture: ComponentFixture<FellowshipAddEditModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        FellowshipAddEditModalComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(FellowshipAddEditModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
