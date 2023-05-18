import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { IRotationModel, IRotationReadOnlyModel } from '../../api';
import { IFormErrors } from '../../shared/common';
import { RotationService } from '../../api';
import {
  GetGraduateMedicalEducationList,
  GetGraduateMedicalEducationDetails,
  UpdateGraduateMedicalEducation,
  CreateGraduateMedicalEducation,
  DeleteGraduateMedicalEducation,
  ClearGraduateMedicalEducationErrors,
} from './gme.actions';

export interface IGraduateMedicalEducation {
  gmeRotations: IRotationReadOnlyModel[];
  selectedRotation: IRotationModel | undefined;
  claims: string[];
  errors?: IFormErrors | null;
}

export const GRADUATE_MEDICAL_EDUCATION_STATE_TOKEN =
  new StateToken<IGraduateMedicalEducation>('graduateMedicalEducation');

@State<IGraduateMedicalEducation>({
  name: GRADUATE_MEDICAL_EDUCATION_STATE_TOKEN,
  defaults: {
    gmeRotations: [],
    selectedRotation: undefined,
    claims: [],
    errors: null,
  },
})
@Injectable()
export class GraduateMedicalEducationState {
  constructor(private rotationService: RotationService) {}

  @Action(GetGraduateMedicalEducationList)
  getGraduateMedicalEducationList(
    ctx: StateContext<IGraduateMedicalEducation>
  ) {
    const state = ctx.getState();
    return this.rotationService.retrieveRotationReadOnly_GetByUserId().pipe(
      tap((result: any) => {
        ctx.setState({
          ...state,
          gmeRotations: result,
          errors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        return of(errors);
      })
    );
  }

  @Action(GetGraduateMedicalEducationDetails)
  getGraduateMedicalEducationDetails(
    ctx: StateContext<IGraduateMedicalEducation>,
    payload: { id: number }
  ) {
    const state = ctx.getState();
    const gmeId = payload.id;
    return this.rotationService.retrieveRotation_GetById(gmeId).pipe(
      tap((result: any) => {
        ctx.setState({
          ...state,
          selectedRotation: result,
          errors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        return of(errors);
      })
    );
  }

  @Action(UpdateGraduateMedicalEducation)
  updateGraduateMedicalEducation(
    ctx: StateContext<IGraduateMedicalEducation>,
    { payload }: UpdateGraduateMedicalEducation
  ) {
    const state = ctx.getState();
    return this.rotationService.updateRotation(payload.id, payload).pipe(
      tap((result: IRotationModel) => {
        const readOnlyResult = {
          id: result.id,
          startDate: result.startDate,
          endDate: result.endDate,
          programName: result.programName,
          alternateInstitutionName: result.alternateInstitutionName,
          clinicalLevel: result.clinicalLevel,
          other: result.other,
          nonSurgicalActivity: result.nonSurgicalActivity,
          isInternationalRotation: result.isInternationalRotation,
        };
        const gmeRotations = state.gmeRotations.map((item) =>
          item.id === readOnlyResult.id ? readOnlyResult : item
        );
        ctx.setState({
          ...state,
          gmeRotations: gmeRotations,
          selectedRotation: undefined,
          errors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.setState({
          ...ctx.getState(),
          errors,
        });
        return of(errors);
      })
    );
  }

  @Action(CreateGraduateMedicalEducation)
  createGraduateMedicalEducation(
    ctx: StateContext<IGraduateMedicalEducation>,
    { payload }: CreateGraduateMedicalEducation
  ) {
    const state = ctx.getState();
    return this.rotationService.createRotation(payload).pipe(
      tap((result: IRotationModel) => {
        const readOnlyResult = {
          id: result.id,
          startDate: result.startDate,
          endDate: result.endDate,
          programName: result.programName,
          alternateInstitutionName: result.alternateInstitutionName,
          clinicalLevel: result.clinicalLevel,
          other: result.other,
          nonSurgicalActivity: result.nonSurgicalActivity,
          isInternationalRotation: result.isInternationalRotation,
        };
        ctx.setState({
          ...state,
          gmeRotations: [readOnlyResult, ...state.gmeRotations],
          selectedRotation: undefined,
          errors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.setState({
          ...ctx.getState(),
          errors,
        });
        return of(errors);
      })
    );
  }

  @Action(DeleteGraduateMedicalEducation)
  deleteGraduateMedicalEducation(
    ctx: StateContext<IGraduateMedicalEducation>,
    { payload }: DeleteGraduateMedicalEducation
  ) {
    const state = ctx.getState();
    return this.rotationService.deleteRotation(payload).pipe(
      tap(() => {
        const gmeRotations = state.gmeRotations.filter(
          (item) => item.id !== payload
        );
        ctx.setState({
          ...state,
          gmeRotations: gmeRotations,
          selectedRotation: undefined,
          errors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.setState({
          ...ctx.getState(),
          errors,
        });
        return of(errors);
      })
    );
  }

  @Action(ClearGraduateMedicalEducationErrors)
  clearGraduateMedicalEducationErrors(
    ctx: StateContext<IGraduateMedicalEducation>
  ) {
    ctx.patchState({ errors: null });
  }
}
