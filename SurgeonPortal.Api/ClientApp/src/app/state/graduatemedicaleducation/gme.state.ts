import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, mergeMap, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import {
  IRotationModel,
  IRotationReadOnlyModel,
  IGmeSummaryReadOnlyModel,
} from '../../api';
import { IFormErrors } from '../../shared/common';
import { RotationService, GmeSummaryService } from '../../api';
import {
  GetGraduateMedicalEducationList,
  GetGraduateMedicalEducationDetails,
  UpdateGraduateMedicalEducation,
  CreateGraduateMedicalEducation,
  DeleteGraduateMedicalEducation,
  ClearGraduateMedicalEducationErrors,
  GetGraduateMedicalEducationSummary,
} from './gme.actions';

export interface IGraduateMedicalEducation {
  gmeRotations: IRotationReadOnlyModel[];
  gmeSummary: IGmeSummaryReadOnlyModel[];
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
    gmeSummary: [],
    selectedRotation: undefined,
    claims: [],
    errors: null,
  },
})
@Injectable()
export class GraduateMedicalEducationState {
  constructor(
    private rotationService: RotationService,
    private gmeSummaryService: GmeSummaryService
  ) {}

  @Action(GetGraduateMedicalEducationSummary)
  getGraduateMedicalEducationSummary(
    ctx: StateContext<IGraduateMedicalEducation>
  ) {
    const state = ctx.getState();
    return this.gmeSummaryService.retrieveGmeSummaryReadOnly_GetByUserId().pipe(
      tap((result: any) => {
        console.log('summary', result);

        const level4AndChief = {
          clinicalLevel: 'Clinical Level 4 Totals',
          minStartDate: '',
          maxStartDate: '',
          programName: '',
          clinicalWeeks: this.getTotals(
            result,
            'clinicalWeeks',
            'Clinical Level 4'
          ),
          nonClinicalWeeks: this.getTotals(
            result,
            'nonClinicalWeeks',
            'Clinical Level 4'
          ),
          essentialsWeeks: this.getTotals(
            result,
            'essentialsWeeks',
            'Clinical Level 4'
          ),
          rowStyle: {
            'font-weight': 'bold',
            'background-color': '#335b92',
            color: '#FFF',
          },
        };
        const level5AndChief = {
          clinicalLevel: 'Clinical Level 5 Totals',
          minStartDate: '',
          maxStartDate: '',
          programName: '',
          clinicalWeeks: this.getTotals(
            result,
            'clinicalWeeks',
            'Clinical Level 5'
          ),
          nonClinicalWeeks: this.getTotals(
            result,
            'nonClinicalWeeks',
            'Clinical Level 5'
          ),
          essentialsWeeks: this.getTotals(
            result,
            'essentialsWeeks',
            'Clinical Level 5'
          ),
          rowStyle: {
            'font-weight': 'bold',
            'background-color': '#335b92',
            color: '#FFF',
          },
        };

        const summaryTotals = {
          clinicalLevel: 'Total Weeks',
          minStartDate: '',
          maxStartDate: '',
          programName: '',
          clinicalWeeks: this.getTotals(result, 'clinicalWeeks'),
          nonClinicalWeeks: this.getTotals(result, 'nonClinicalWeeks'),
          essentialsWeeks: this.getTotals(result, 'essentialsWeeks'),
          rowStyle: {
            'font-weight': 'bold',
            'background-color': '#1F3758',
            color: '#FFF',
          },
        };
        const summaryAverages = {
          clinicalLevel: 'Avg Weeks',
          minStartDate: '',
          maxStartDate: '',
          programName: '',
          clinicalWeeks: this.getAverages(result, 'clinicalWeeks'),
          nonClinicalWeeks: this.getAverages(result, 'nonClinicalWeeks'),
          essentialsWeeks: this.getAverages(result, 'essentialsWeeks'),
          rowStyle: {
            'font-weight': 'bold',
            'background-color': '#1F3758',
            color: '#FFF',
          },
        };
        result.push(level4AndChief);
        result.push(level5AndChief);
        result.sort((a: any, b: any) =>
          a.clinicalLevel > b.clinicalLevel ? 1 : -1
        );
        result.push(summaryTotals);
        result.push(summaryAverages);

        ctx.setState({
          ...state,
          gmeSummary: result,
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

  @Action(GetGraduateMedicalEducationList)
  getGraduateMedicalEducationList(
    ctx: StateContext<IGraduateMedicalEducation>
  ) {
    const state = ctx.getState();
    return this.rotationService.retrieveRotationReadOnly_GetByUserId().pipe(
      tap((result: any) => {
        console.log('rotations', result);
        ctx.setState({
          ...state,
          gmeRotations: result.sort((a: any, b: any) =>
            a.startDate > b.startDate ? 1 : -1
          ),
          errors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        return of(errors);
      }),
      mergeMap(() => ctx.dispatch(new GetGraduateMedicalEducationSummary()))
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
          clinicalLevelId: result.clinicalLevelId,
          clinicalActivity: result.clinicalActivity,
          other: result.other,
          nonSurgicalActivity: result.nonSurgicalActivity,
          isInternationalRotation: result.isInternationalRotation,
          isCredit: result.isCredit,
          isEssential: result.isEssential,
        };
        const gmeRotations = state.gmeRotations.map((item) =>
          item.id === readOnlyResult.id ? readOnlyResult : item
        );
        ctx.setState({
          ...state,
          gmeRotations: gmeRotations.sort((a, b) =>
            a.startDate > b.startDate ? 1 : -1
          ),
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
      }),
      mergeMap(() => ctx.dispatch(new GetGraduateMedicalEducationSummary()))
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
          clinicalLevelId: result.clinicalLevelId,
          clinicalActivity: result.clinicalActivity,
          other: result.other,
          nonSurgicalActivity: result.nonSurgicalActivity,
          isInternationalRotation: result.isInternationalRotation,
          isCredit: result.isCredit,
          isEssential: result.isEssential,
        };
        ctx.setState({
          ...state,
          gmeRotations: [readOnlyResult, ...state.gmeRotations].sort((a, b) =>
            a.startDate > b.startDate ? 1 : -1
          ),
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
      }),
      mergeMap(() => ctx.dispatch(new GetGraduateMedicalEducationSummary()))
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
          gmeRotations: gmeRotations.sort((a, b) =>
            a.startDate > b.startDate ? 1 : -1
          ),
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
      }),
      mergeMap(() => ctx.dispatch(new GetGraduateMedicalEducationSummary()))
    );
  }

  @Action(ClearGraduateMedicalEducationErrors)
  clearGraduateMedicalEducationErrors(
    ctx: StateContext<IGraduateMedicalEducation>
  ) {
    ctx.patchState({ errors: null });
  }

  getTotals(items: any[], prop: string, filter?: string) {
    return items.reduce((a, b) => {
      if (filter) {
        if (b[prop] && b.clinicalLevel.startsWith(filter)) {
          return a + parseInt(b[prop]);
        }
        return a;
      } else {
        if (b[prop]) {
          return a + parseInt(b[prop]);
        }
        return a;
      }
    }, 0);
  }
  getAverages(items: any[], prop: string) {
    const total = this.getTotals(items, prop);
    const avg = total / items.length;
    return Math.round(avg * 10) / 10;
  }
}
