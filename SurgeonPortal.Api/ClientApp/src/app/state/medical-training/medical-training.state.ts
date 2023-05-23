import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { IMedicalTrainingModel } from 'src/app/api/models/medicaltraining/medical-training.model';
import { IFormErrors } from 'src/app/shared/common';
import { MedicalTrainingService } from 'src/app/api/services/medicaltraining/medical-training.service';
import {
  CreateMedicalTraining,
  GetAdvancedTrainingData,
  GetMedicalTraining,
  UpdateMedicalTraining,
} from './medical-training.actions';
import { AdvancedTrainingService } from 'src/app/api/services/medicaltraining/advanced-training.service';
import { IAdvancedTrainingReadOnlyModel } from 'src/app/api/models/medicaltraining/advanced-training-read-only.model';

export interface IMedicalTraining {
  medicalTraining: IMedicalTrainingModel | undefined;
  additionalTraining: IAdvancedTrainingReadOnlyModel[] | undefined;
  errors?: IFormErrors | undefined;
}

export const MEDICALSTATE_STATE_TOKEN = new StateToken<IMedicalTraining>(
  'medical_training'
);

@State<IMedicalTraining>({
  name: MEDICALSTATE_STATE_TOKEN,
  defaults: {
    medicalTraining: undefined,
    additionalTraining: undefined,
  },
})
@Injectable()
export class MedicalTrainingState {
  constructor(
    private medicalTrainingService: MedicalTrainingService,
    private advancedTrainingService: AdvancedTrainingService
  ) {}

  @Action(GetMedicalTraining)
  getMedicalTraining(
    ctx: StateContext<IMedicalTraining>
  ): Observable<IMedicalTrainingModel | undefined> {
    if (ctx.getState()?.medicalTraining) {
      return of(ctx.getState()?.medicalTraining);
    }

    return this.medicalTrainingService
      .retrieveMedicalTraining_GetByUserId()
      .pipe(
        tap((medicalTraining: IMedicalTrainingModel) => {
          ctx.patchState({
            medicalTraining,
          });
        })
      );
  }

  @Action(CreateMedicalTraining)
  createMedicalTraining(
    ctx: StateContext<IMedicalTraining>,
    action: CreateMedicalTraining
  ): Observable<IMedicalTrainingModel | undefined> {
    return this.medicalTrainingService
      .createMedicalTraining(action.payload)
      .pipe(
        tap((medicalTraining: IMedicalTrainingModel) => {
          ctx.patchState({
            medicalTraining,
          });
        })
      );
  }

  @Action(UpdateMedicalTraining)
  updateMedicalTraining(
    ctx: StateContext<IMedicalTraining>,
    action: CreateMedicalTraining
  ): Observable<IMedicalTrainingModel | undefined> {
    return this.medicalTrainingService
      .updateMedicalTraining(action.payload)
      .pipe(
        tap((medicalTraining: IMedicalTrainingModel) => {
          ctx.patchState({
            medicalTraining,
          });
        })
      );
  }

  @Action(GetAdvancedTrainingData)
  getAdvancedTrainingData(
    ctx: StateContext<IMedicalTraining>
  ): Observable<IAdvancedTrainingReadOnlyModel[] | undefined> {
    if (ctx.getState()?.additionalTraining) {
      return of(ctx.getState()?.additionalTraining);
    }

    return this.advancedTrainingService
      .retrieveAdvancedTrainingReadOnly_GetByUserId()
      .pipe(
        tap((additionalTraining: IAdvancedTrainingReadOnlyModel[]) => {
          ctx.patchState({
            additionalTraining,
          });
        })
      );
  }
}
