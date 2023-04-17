import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxsModule } from '@ngxs/store';
import { MedicalTrainingComponent } from './medical-training.component';
import { surgeonPortalState } from '../state/surgeon-portal.state';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('MedicalTrainingComponent', () => {
  let component: MedicalTrainingComponent;
  let fixture: ComponentFixture<MedicalTrainingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        MedicalTrainingComponent,
        NgxsModule.forRoot(surgeonPortalState),
        HttpClientTestingModule,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(MedicalTrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
