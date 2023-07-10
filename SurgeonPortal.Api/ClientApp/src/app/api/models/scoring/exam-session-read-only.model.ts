
export interface IExamSessionReadOnlyModel {
    examScheduleId: number;
    firstName: string;
    lastName: string;
    startTime: string;
    endTime: string;
    meetingLink: string;
    isSubmitted: boolean;
    isCurrentSession: boolean;
    sessionNumber: number;
}
