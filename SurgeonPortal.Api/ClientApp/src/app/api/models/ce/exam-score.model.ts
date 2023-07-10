
export interface IExamScoreModel {
    examScheduleScoreId: number;
    examScheduleId: number;
    examinerUserId: number;
    examinerScore: number;
    submitted: boolean;
    submittedDateTimeUTC: string;
    createdByUserId: number;
    createdAtUtc: string;
    lastUpdatedByUserId: number;
    lastUpdatedAtUtc: string;
}
