import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormControl, FormGroup } from '@angular/forms';
import { catchError, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import {
  IAdditionalTrainingModel,
  IAdditionalTrainingReadOnlyModel,
} from '../../api';
import { IFormErrors } from '../../shared/common';
import { AdditionalTrainingsService } from '../../api';
import {
  GetAdditionalTrainingList,
  GetAdditionalTrainingDetails,
  UpdateAdditionalTraining,
  CreateAdditionalTraining,
} from './examination-gq-additional-training.actions';

export interface IGQAdditionalTraining {
  AdditionalTraining: IAdditionalTrainingReadOnlyModel[];
  selectedAdditionalTraining: IAdditionalTrainingModel | undefined;
  claims: string[];
  errors?: IFormErrors | null;
}

export const GQ_ADDITIONAL_TRAINING_STATE_TOKEN =
  new StateToken<IGQAdditionalTraining>('gqAdditionalTraining');

@State<IGQAdditionalTraining>({
  name: GQ_ADDITIONAL_TRAINING_STATE_TOKEN,
  defaults: {
    AdditionalTraining: [],
    selectedAdditionalTraining: undefined,
    claims: [],
    errors: null,
  },
})
@Injectable()
export class GQAdditionalTrainingState {
  constructor(private additionalTrainingsService: AdditionalTrainingsService) {}

  @Action(GetAdditionalTrainingList)
  getAdditionalTrainingList(
    ctx: StateContext<IGQAdditionalTraining>,
    payload: { userId: number }
  ) {
    const state = ctx.getState();
    const userId = payload.userId;
    return this.additionalTrainingsService
      .retrieveAdditionalTrainingReadOnly_GetAllByUserId(userId)
      .pipe(
        tap((result: any) => {
          ctx.setState({
            ...state,
            AdditionalTraining: result,
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

  @Action(GetAdditionalTrainingDetails)
  getAdditionalTrainingDetails(
    ctx: StateContext<IGQAdditionalTraining>,
    payload: { trainingId: number }
  ) {
    const state = ctx.getState();
    const trainingId = payload.trainingId;
    return this.additionalTrainingsService
      .retrieveAdditionalTraining_GetByTrainingId(trainingId)
      .pipe(
        tap((result: any) => {
          ctx.setState({
            ...state,
            selectedAdditionalTraining: result,
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

  @Action(UpdateAdditionalTraining)
  updateAdditionalTraining(
    ctx: StateContext<IGQAdditionalTraining>,
    { payload }: UpdateAdditionalTraining
  ) {
    const state = ctx.getState();
    return this.additionalTrainingsService
      .updateAdditionalTraining(payload.trainingId, payload)
      .pipe(
        tap((result: IAdditionalTrainingModel) => {
          const readOnlyResult = {
            trainingId: result.trainingId,
            typeOfTraining: result.typeOfTraining,
            state: result.state,
            city: result.city,
            institutionName: result.institutionName,
            other: result.other,
            dateStarted: result.dateStarted,
            dateEnded: result.dateEnded,
          };
          const additionalTraining = state.AdditionalTraining.map((item) =>
            item.trainingId === readOnlyResult.trainingId
              ? readOnlyResult
              : item
          );
          ctx.setState({
            ...state,
            AdditionalTraining: additionalTraining,
            selectedAdditionalTraining: undefined,
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

  @Action(CreateAdditionalTraining)
  createAdditionalTraining(
    ctx: StateContext<IGQAdditionalTraining>,
    { payload }: CreateAdditionalTraining
  ) {
    const state = ctx.getState();
    return this.additionalTrainingsService
      .createAdditionalTraining(payload)
      .pipe(
        tap((result: IAdditionalTrainingModel) => {
          const readOnlyResult = {
            trainingId: result.trainingId,
            typeOfTraining: result.typeOfTraining,
            state: result.state,
            city: result.city,
            institutionName: result.institutionName,
            other: result.other,
            dateStarted: result.dateStarted,
            dateEnded: result.dateEnded,
          };
          ctx.setState({
            ...state,
            AdditionalTraining: [readOnlyResult, ...state.AdditionalTraining],
            selectedAdditionalTraining: undefined,
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
}
