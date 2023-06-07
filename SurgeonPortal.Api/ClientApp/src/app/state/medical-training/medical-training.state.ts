import { Injectable } from '@angular/core';
import { tap, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import { IMedicalTrainingModel } from 'src/app/api/models/medicaltraining/medical-training.model';
import { IFormErrors } from 'src/app/shared/common';
import { MedicalTrainingService } from 'src/app/api/services/medicaltraining/medical-training.service';
import {
  CreateMedicalTraining,
  GetAdvancedTrainingData,
  GetMedicalTraining,
  GetUserCertificates,
  GetOtherCertifications,
  UpdateMedicalTraining,
  CreateOtherCertification,
  UpdateOtherCertifications,
} from './medical-training.actions';
import { AdvancedTrainingService } from 'src/app/api/services/medicaltraining/advanced-training.service';
import { IAdvancedTrainingReadOnlyModel } from 'src/app/api/models/medicaltraining/advanced-training-read-only.model';
import { IUserCertificateReadOnlyModel } from 'src/app/api/models/medicaltraining/user-certificate-read-only.model';
import { UserCertificateService } from 'src/app/api/services/medicaltraining/user-certificate.service';
import { OtherCertificationsService } from 'src/app/api/services/medicaltraining/other-certifications.service';
import { IOtherCertificationsReadOnlyModel } from 'src/app/api/models/medicaltraining/other-certifications-read-only.model';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

export interface IMedicalTraining {
  medicalTraining: IMedicalTrainingModel | undefined;
  additionalTraining: IAdvancedTrainingReadOnlyModel[] | undefined;
  userCertificates: IUserCertificateReadOnlyModel[] | undefined;
  otherCertifications: IOtherCertificationsReadOnlyModel[] | undefined;
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
    userCertificates: undefined,
    otherCertifications: undefined,
  },
})
@Injectable()
export class MedicalTrainingState {
  constructor(
    private medicalTrainingService: MedicalTrainingService,
    private advancedTrainingService: AdvancedTrainingService,
    private userCertificateService: UserCertificateService,
    private otherCertificationsService: OtherCertificationsService,
    private globalDialogService: GlobalDialogService
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
        }),
        catchError((error) => {
          console.error('------- In Medical Training Store', error);
          console.error(error);
          return of(error);
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
          this.globalDialogService.showSuccessError(
            'Success',
            'Created successfully',
            true
          );
        }),
        catchError((error) => {
          console.error('------- In Medical Training Store', error);
          console.error(error);
          return of(error);
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
          this.globalDialogService.showSuccessError(
            'Success',
            'Updated successfully',
            true
          );
        }),
        catchError((error) => {
          console.error('------- In Medical Training Store', error);
          console.error(error);
          return of(error);
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
        }),
        catchError((error) => {
          console.error('------- In Medical Training Store', error);
          console.error(error);
          return of(error);
        })
      );
  }

  @Action(GetUserCertificates)
  getUserCertificates(
    ctx: StateContext<IMedicalTraining>,
    payload: GetUserCertificates
  ): Observable<IUserCertificateReadOnlyModel[] | undefined> {
    if (ctx.getState()?.userCertificates && !payload?.isUpload) {
      return of(ctx.getState()?.userCertificates);
    }
    return this.userCertificateService
      .retrieveUserCertificateReadOnly_GetByUserId()
      .pipe(
        tap((userCertificates: IUserCertificateReadOnlyModel[]) => {
          ctx.patchState({
            userCertificates,
          });
        }),
        catchError((error) => {
          console.error('------- In Medical Training Store', error);
          console.error(error);
          return of(error);
        })
      );
  }

  @Action(GetOtherCertifications)
  GetOtherCertifications(
    ctx: StateContext<IMedicalTraining>,
    payload: GetOtherCertifications
  ): Observable<IOtherCertificationsReadOnlyModel[] | undefined> {
    if (ctx.getState()?.otherCertifications && !payload?.isUpdate) {
      return of(ctx.getState()?.otherCertifications);
    }

    return this.otherCertificationsService
      .retrieveOtherCertificationsReadOnly_GetByUserId()
      .pipe(
        tap((otherCertifications: IOtherCertificationsReadOnlyModel[]) => {
          ctx.patchState({
            otherCertifications,
          });
        }),
        catchError((error) => {
          console.error('------- In Medical Training Store', error);
          console.error(error);
          return of(error);
        })
      );
  }

  @Action(CreateOtherCertification)
  createOtherCertification(
    ctx: StateContext<IMedicalTraining>,
    payload: CreateOtherCertification
  ): Observable<void> {
    return this.otherCertificationsService
      .createOtherCertifications(payload.model)
      .pipe(
        tap(() => {
          ctx.dispatch(new GetOtherCertifications(true));
          this.globalDialogService.showSuccessError(
            'Success',
            'Created successfully',
            true
          );
        }),
        catchError((error) => {
          console.error('------- In Medical Training Store', error);
          console.error(error);
          this.globalDialogService.showSuccessError(
            'Error',
            'Create failed',
            false
          );
          return of(error);
        })
      );
  }

  @Action(UpdateOtherCertifications)
  updateOtherCertifications(
    ctx: StateContext<IMedicalTraining>,
    payload: UpdateOtherCertifications
  ): Observable<void> {
    return this.otherCertificationsService
      .updateOtherCertifications(payload.model.id, payload.model)
      .pipe(
        tap(() => {
          ctx.dispatch(new GetOtherCertifications(true));
          this.globalDialogService.showSuccessError(
            'Success',
            'Saved successfully',
            true
          );
        }),
        catchError((error) => {
          console.error('------- In Medical Training Store', error);
          console.error(error);
          this.globalDialogService.showSuccessError(
            'Error',
            'Save failed',
            false
          );
          return of(error);
        })
      );
  }
}
