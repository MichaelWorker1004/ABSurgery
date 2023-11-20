
export interface IRosterReadOnlyModel {
    examScheduleId: number;
    dayNumber: number;
    sessionNumber: number;
    roster: string;
    displayName: string;
    isSubmitted: boolean;
    examDate: string;
}
