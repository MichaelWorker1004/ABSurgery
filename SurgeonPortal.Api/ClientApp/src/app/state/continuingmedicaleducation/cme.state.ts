import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, map, share, tap } from 'rxjs/operators';
import { Observable, forkJoin, of } from 'rxjs';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import {
  CmeService,
  ICmeAdjustmentReadOnlyModel,
  ICmeCreditReadOnlyModel,
} from '../../api';
import { IFormErrors } from '../../shared/common';
import {
  GetCMECredits,
  GetCMEAdjustments,
  GetCMECreditDetails,
  ClearCMEErrors,
  GetCmeSummary,
} from './cme.actions';

export interface ICmeSummaryRow {
  rowLabel: string;
  credits: number | string;
  saCredits: number | string;
  rowStyle?: any;
}

export interface IDroppingCmeCredits {
  credits: number;
  saCredits: number;
}

export interface ICmeCredit extends ICmeCreditReadOnlyModel {
  credits: number;
  cmeDirect: string;
  rowStyle?: any;
}
export interface ICmeAdjustment extends ICmeAdjustmentReadOnlyModel {
  credits: number;
  rowStyle?: any;
}

export interface IContinuingMedicalEducation {
  cmeCredits: ICmeCredit[];
  cmeAdjustments: ICmeAdjustment[];
  cmeSummary: ICmeSummaryRow[];
  cmeDroppingCredits: IDroppingCmeCredits | undefined;
  selectedCmeCredit: ICmeCredit | undefined;
  claims: string[];
  errors?: IFormErrors | null;
}

export const CME_STATE_TOKEN = new StateToken<IContinuingMedicalEducation>(
  'ContinuingMedicalEducation'
);

@State<IContinuingMedicalEducation>({
  name: CME_STATE_TOKEN,
  defaults: {
    cmeCredits: [],
    cmeAdjustments: [],
    cmeSummary: [],
    cmeDroppingCredits: undefined,
    selectedCmeCredit: undefined,
    claims: [],
    errors: null,
  },
})
@Injectable()
export class ContinuingMedicalEducationState {
  constructor(private cmeService: CmeService) {}

  @Action(GetCmeSummary)
  getCmeSummary(
    ctx: StateContext<IContinuingMedicalEducation>
  ): Observable<IContinuingMedicalEducation> {
    const joins = [
      this.getCMECredits(ctx).pipe(catchError((error) => of(error))),
      this.getCMEAdjustments(ctx).pipe(catchError((error) => of(error))),
    ];

    return forkJoin(joins).pipe(
      map((ContinuingMedicalEducation: IContinuingMedicalEducation[]) => {
        const cmeCredits = ctx.getState().cmeCredits;
        const cmeAdjustments = ctx.getState().cmeAdjustments;
        const cmeSummary = this.calculateCmeSummary(cmeCredits, cmeAdjustments);
        const cmeDroppingCredits = this.calculateCmeDroppingCredits(
          cmeCredits,
          cmeAdjustments
        );
        ctx.patchState({
          cmeSummary: cmeSummary,
          cmeDroppingCredits: cmeDroppingCredits,
        });
        return of(ctx.getState());
      }),
      share(),
      catchError((error) => {
        console.error('------- In CME Store', error);
        return of(error);
      })
    );
  }

  @Action(GetCMECredits)
  getCMECredits(ctx: StateContext<IContinuingMedicalEducation>) {
    // const state = ctx.getState();
    return this.cmeService.retrieveCmeCreditReadOnly_GetByUserId().pipe(
      tap((response) => {
        const creditsList = [] as ICmeCredit[];
        response.forEach((credit) => {
          let rowStyle = undefined;
          if (
            credit.creditExpDate &&
            new Date(credit.creditExpDate) < new Date()
          ) {
            rowStyle = { color: '#8b040a' };
          }

          creditsList.push({
            ...credit,
            credits: credit.creditsTotal,
            cmeDirect: credit.cMEDirect === 0 ? 'No' : 'Yes',
            rowStyle: rowStyle,
          });
        });
        ctx.patchState({
          cmeCredits: creditsList.sort((a, b) => {
            return (
              new Date(b.date).getTime() - new Date(a.date).getTime() ||
              b.cmeId - a.cmeId
            );
          }),
          errors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors: errors });
        return of(errors);
      })
    );
  }

  @Action(GetCMECreditDetails)
  getCMECreditDetails(
    ctx: StateContext<IContinuingMedicalEducation>,
    payload: GetCMECreditDetails
  ) {
    // const state = ctx.getState();
    return this.cmeService.retrieveCmeCreditReadOnly_GetById(payload.id).pipe(
      tap((response) => {
        ctx.patchState({
          selectedCmeCredit: {
            ...response,
            credits: response.creditsTotal,
            cmeDirect: response.cMEDirect === 0 ? 'No' : 'Yes',
          },
          errors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors: errors });
        return of(errors);
      })
    );
  }

  @Action(GetCMEAdjustments)
  getCMEAdjustments(ctx: StateContext<IContinuingMedicalEducation>) {
    // const state = ctx.getState();
    return this.cmeService.retrieveCmeAdjustmentReadOnly_GetByUserId().pipe(
      tap((response) => {
        const adjustmentsList = [] as ICmeAdjustment[];
        response.forEach((adjustment) => {
          let rowStyle = undefined;
          if (
            adjustment.creditExpDate &&
            new Date(adjustment.creditExpDate) < new Date()
          ) {
            rowStyle = { color: '#8b040a' };
          }

          adjustmentsList.push({
            ...adjustment,
            credits: adjustment.creditsTotal,
            rowStyle: rowStyle,
          });
        });
        ctx.patchState({
          cmeAdjustments: adjustmentsList.sort((a, b) => {
            return (
              new Date(b.date).getTime() - new Date(a.date).getTime() ||
              b.cmeId - a.cmeId
            );
          }),
          errors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors: errors });
        return of(errors);
      })
    );
  }

  @Action(ClearCMEErrors)
  clearCMEErrors(ctx: StateContext<IContinuingMedicalEducation>) {
    ctx.patchState({
      errors: null,
    });
  }

  private calculateCmeSummary(
    credits: ICmeCredit[],
    adjustments: ICmeAdjustment[]
  ) {
    const totalCreditsRequired = 150;
    const totalSACreditsRequired = 50;
    let creditsAdjustment = 0;
    let saCreditsAdjustment = 0;
    let creditsEarned = 0;
    let saCreditsEarned = 0;

    if (adjustments.length > 0) {
      adjustments.forEach((adjustment) => {
        if (adjustment.creditExpDate) {
          if (new Date(adjustment.creditExpDate) < new Date()) return;
        }
        saCreditsAdjustment += adjustment.creditsSA;
        creditsAdjustment += adjustment.creditsTotal;
      });
    }
    if (credits.length > 0) {
      credits.forEach((credit) => {
        if (credit.creditExpDate) {
          if (new Date(credit.creditExpDate) < new Date()) return;
        }
        saCreditsEarned += credit.creditsSA;
        creditsEarned += credit.creditsTotal;
      });
    }

    const requiredCredits = totalCreditsRequired - creditsAdjustment;
    const requiredSACredits = totalSACreditsRequired - saCreditsAdjustment;
    const remainingCredits =
      totalCreditsRequired - creditsAdjustment - creditsEarned;
    const remainingSACredits =
      totalSACreditsRequired - saCreditsAdjustment - saCreditsEarned;

    const summary: ICmeSummaryRow[] = [
      {
        rowLabel: 'ABS Requirements',
        credits: totalCreditsRequired,
        saCredits: totalSACreditsRequired,
      },
      {
        rowLabel: 'Your ABS Waivers',
        credits: `(${creditsAdjustment})`,
        saCredits: `(${saCreditsAdjustment})`,
      },
      {
        rowLabel: 'YOUR REQUIREMENTS',
        credits: requiredCredits >= 0 ? requiredCredits : 0,
        saCredits: requiredSACredits >= 0 ? requiredSACredits : 0,
        rowStyle: {
          'font-weight': 'bold',
          color: '#000',
        },
      },
      {
        rowLabel: 'Credits-to-date',
        credits: creditsEarned,
        saCredits: saCreditsEarned,
      },
      {
        rowLabel: 'CREDITS NEEDED',
        credits: remainingCredits >= 0 ? remainingCredits : 0,
        saCredits: remainingSACredits >= 0 ? remainingSACredits : 0,
        rowStyle: {
          'font-weight': 'bold',
          color: '#8b040a',
        },
      },
    ];
    return summary;
  }

  private calculateCmeDroppingCredits(
    credits: ICmeCredit[],
    adjustments: ICmeAdjustment[]
  ) {
    const droppingCredits: IDroppingCmeCredits = {
      credits: 0,
      saCredits: 0,
    };
    const today = new Date();
    const dropDateEnd = new Date(today.getFullYear(), 11, 31);
    if (credits.length > 0) {
      credits.forEach((credit) => {
        if (credit.creditExpDate) {
          const expirationDate = new Date(credit.creditExpDate);
          if (expirationDate >= today && expirationDate <= dropDateEnd) {
            droppingCredits.credits += credit.creditsTotal;
            droppingCredits.saCredits += credit.creditsSA;
          }
        }
      });
    }
    if (adjustments.length > 0) {
      adjustments.forEach((adjustment) => {
        if (adjustment.creditExpDate) {
          const expirationDate = new Date(adjustment.creditExpDate);
          if (expirationDate >= today && expirationDate <= dropDateEnd) {
            droppingCredits.credits += adjustment.creditsTotal;
            droppingCredits.saCredits += adjustment.creditsSA;
          }
        }
      });
    }
    return droppingCredits;
  }
}
