// import { Injectable } from '@angular/core';
// import { HttpErrorResponse } from '@angular/common/http';
// import { FormControl, FormGroup } from '@angular/forms';
// import { catchError, tap } from 'rxjs/operators';
// import { of } from 'rxjs';
// import { Action, State, StateContext, StateToken } from '@ngxs/store';
// import { IFormErrors } from '../../shared/common';
// import { ExamProcessService, IExamProcessReadOnlyModel } from '../../api';
// import { GetAvailableExams } from './exam-process.actions';

// export interface IExamProcess {
//   availableExams: IExamProcessReadOnlyModel[];
//   errors?: IFormErrors | null;
// }

// export const EXAM_PROCESS_STATE_TOKEN = new StateToken<IExamProcess>(
//   'examProcess'
// );

// @State<IExamProcess>({
//   name: EXAM_PROCESS_STATE_TOKEN,
//   defaults: {
//     availableExams: [],
//     errors: null,
//   },
// })
// @Injectable()
// export class ExamProcessState {
//   constructor(private examProcessService: ExamProcessService) {}

//   @Action(GetAvailableExams)
//   getExamHistoryList(
//     ctx: StateContext<IExamProcess>,
//     payload: { userId: number }
//   ) {
//     const state = ctx.getState();
//     const userId = payload.userId;
//     return this.examProcessService
//       .retrieveExamProcessReadOnly_GetAllByUserId(userId)
//       .pipe(
//         tap((result: any) => {
//           ctx.setState({
//             ...state,
//             availableExams: result,
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
// }
