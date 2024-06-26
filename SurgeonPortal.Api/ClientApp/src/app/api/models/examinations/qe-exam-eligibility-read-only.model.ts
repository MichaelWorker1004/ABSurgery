
export interface IQeExamEligibilityReadOnlyModel {
    examId: number;
    examCode: string;
    examName: string;
    appOpenDate: string;
    appCloseDate: string;
    appLateDate: string;
    appDelayDate: string;
    examStartDate: string;
    examEndDate: string;
    applicationApproved: number;
    examRegistrationAvailable: number;
    registrationOpen: number;
    admissionCardReport: string;
    userApplicationExists: number;
}
