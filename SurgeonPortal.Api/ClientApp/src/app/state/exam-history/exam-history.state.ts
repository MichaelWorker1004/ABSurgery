// import { Injectable } from '@angular/core';
// import { HttpErrorResponse } from '@angular/common/http';
// import { FormControl, FormGroup } from '@angular/forms';
// import { catchError, tap } from 'rxjs/operators';
// import { of } from 'rxjs';
// import { Action, State, StateContext, StateToken } from '@ngxs/store';
// import { IFormErrors } from '../../shared/common';
// import {
//   ExamHistoryService,
//   IExamHistoryReadOnlyModel,
//   IActiveExamsReadOnlyModel,
//   IExamDetailsReadOnlyModel,
// } from '../../api';
// import {
//   GetExamHistory,
//   GetActiveExams,
//   GetExamDetails,
// } from './exam-history.actions';

// export interface IExpandableExamHistory extends IExamHistoryReadOnlyModel {
//   examId: number;
//   expandedDetails: IExamDetailsReadOnlyModel;
// }

// export interface IExamHistory {
//   activeExams: IActiveExamsReadOnlyModel[];
//   examHistory: IExpandableExamHistory[];
//   errors?: IFormErrors | null;
// }

// export const EXAM_HISTORY_STATE_TOKEN = new StateToken<IExamHistory>(
//   'examHistory'
// );

// @State<IExamHistory>({
//   name: EXAM_HISTORY_STATE_TOKEN,
//   defaults: {
//     activeExams: [],
//     examHistory: [],
//     errors: null,
//   },
// })
// @Injectable()
// export class ExamHistoryState {
//   constructor(private examHistoryService: ExamHistoryService) {}

//   @Action(GetExamHistory)
//   getExamHistoryList(
//     ctx: StateContext<IExamHistory>,
//     payload: { userId: number }
//   ) {
//     const state = ctx.getState();
//     const userId = payload.userId;
//     return this.examHistoryService
//       .retrieveExamHistoryReadOnly_GetAllByUserId(userId)
//       .pipe(
//         tap((result: any) => {
//           ctx.setState({
//             ...state,
//             examHistory: result,
//             errors: null,
//           });
//         }),
//         catchError((httpError: HttpErrorResponse) => {
//           const errors = httpError.error;
//           ctx.patchState({ errors });
//           return of(errors);
//         })
//       );
//   }

//   @Action(GetActiveExams)
//   getActiveExamList(
//     ctx: StateContext<IExamHistory>,
//     payload: { userId: number }
//   ) {
//     const state = ctx.getState();
//     const userId = payload.userId;
//     return this.examHistoryService
//       .retrieveActiveExamsReadOnly_GetAllByUserId(userId)
//       .pipe(
//         tap((result: any) => {
//           ctx.setState({
//             ...state,
//             activeExams: result,
//             errors: null,
//           });
//         }),
//         catchError((httpError: HttpErrorResponse) => {
//           const errors = httpError.error;
//           ctx.patchState({ errors });
//           return of(errors);
//         })
//       );
//   }

//   @Action(GetExamDetails)
//   getExamDetails(ctx: StateContext<IExamHistory>, payload: { examId: number }) {
//     const state = ctx.getState();
//     const examId = payload.examId;
//     return this.examHistoryService.retrieveExamReadOnly_ByExamId(examId).pipe(
//       tap((result: any) => {
//         const examHistory = state.examHistory.map((item) => {
//           if (item.examId === examId) {
//             return { ...item, expandedDetails: result };
//           }
//           return item;
//         });

//         ctx.setState({
//           ...state,
//           examHistory: examHistory,
//           errors: null,
//         });
//       }),
//       catchError((httpError: HttpErrorResponse) => {
//         const errors = httpError.error;
//         ctx.patchState({ errors });
//         return of(errors);
//       })
//     );
//   }
// }
