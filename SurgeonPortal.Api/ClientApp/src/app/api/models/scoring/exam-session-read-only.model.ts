
export interface IExamSessionReadOnlyModel {
    firstName: string;
    lastName: string;
    startTime: string;
    endTime: string;
    meetingLink: string;
    isSubmitted: boolean;
    isCurrentSession: boolean;
    sessionNumber: number;
}
