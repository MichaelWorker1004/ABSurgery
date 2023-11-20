
export interface IExamineeReadOnlyModel {
    examScheduleId: number;
    fullName: string;
    examDate: string;
    cases: any[];
    examineeUserId: number;
    examScoringId: number;
    timerBit: boolean;
}
