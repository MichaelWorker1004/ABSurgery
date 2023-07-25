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
  ClearGraduateMedicalEducationDetails,
} from './gme.actions';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

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
    private gmeSummaryService: GmeSummaryService,
    private globalDialogService: GlobalDialogService
  ) {}

  @Action(GetGraduateMedicalEducationSummary)
  getGraduateMedicalEducationSummary(
    ctx: StateContext<IGraduateMedicalEducation>
  ) {
    const state = ctx.getState();
    return this.gmeSummaryService.retrieveGmeSummaryReadOnly_GetByUserId().pipe(
      tap((result: any) => {
        ctx.setState({
          ...state,
          gmeSummary: this.buildSummaryRows(result),
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
    //const state = ctx.getState();
    const gmeId = payload.id;
    return this.rotationService.retrieveRotation_GetById(gmeId).pipe(
      tap((result: any) => {
        ctx.patchState({
          selectedRotation: result,
          //errors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        return of(errors);
      })
    );
  }

  @Action(ClearGraduateMedicalEducationDetails)
  clearGraduateMedicalEducationDetails(
    ctx: StateContext<IGraduateMedicalEducation>
  ) {
    ctx.patchState({
      selectedRotation: undefined,
    });
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
        this.globalDialogService.showSuccessError(
          'Success',
          'Rotation Updated Successfully',
          true
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
        this.globalDialogService.showSuccessError(
          'Success',
          'Rotation Created Successfully',
          true
        );
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

  buildSummaryRows(items: any[]) {
    let allRows = [];
    const level4AndChief = {
      clinicalLevel: 'Clinical Level 4 Totals',
      minStartDate: '',
      maxStartDate: '',
      programName: '',
      clinicalWeeks: this.getTotals(items, 'clinicalWeeks', 'Clinical Level 4'),
      nonClinicalWeeks: this.getTotals(
        items,
        'nonClinicalWeeks',
        'Clinical Level 4'
      ),
      essentialsWeeks: this.getTotals(
        items,
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
      clinicalWeeks: this.getTotals(items, 'clinicalWeeks', 'Clinical Level 5'),
      nonClinicalWeeks: this.getTotals(
        items,
        'nonClinicalWeeks',
        'Clinical Level 5'
      ),
      essentialsWeeks: this.getTotals(
        items,
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
      clinicalWeeks: this.getTotals(items, 'clinicalWeeks'),
      nonClinicalWeeks: this.getTotals(items, 'nonClinicalWeeks'),
      essentialsWeeks: this.getTotals(items, 'essentialsWeeks'),
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
      clinicalWeeks: this.getAverages(items, 'clinicalWeeks'),
      nonClinicalWeeks: this.getAverages(items, 'nonClinicalWeeks'),
      essentialsWeeks: this.getAverages(items, 'essentialsWeeks'),
      rowStyle: {
        'font-weight': 'bold',
        'background-color': '#1F3758',
        color: '#FFF',
      },
    };
    allRows = [...items, level4AndChief, level5AndChief];
    allRows.sort((a: any, b: any) =>
      a.clinicalLevel > b.clinicalLevel ? 1 : -1
    );
    allRows.push(summaryTotals);
    allRows.push(summaryAverages);

    return allRows;
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
