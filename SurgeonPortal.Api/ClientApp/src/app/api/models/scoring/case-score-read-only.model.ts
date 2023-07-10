
export interface ICaseScoreReadOnlyModel {
    examScoringId: number;
    examCaseId: number;
    examinerUserId: number;
    examineeUserId: number;
    examineeFirstName: string;
    examineeMiddleName: string;
    examineeLastName: string;
    examineeSuffix: string;
    examDate: string;
    startTime: string;
    endTime: string;
    score: number;
    criticalFail: boolean;
    remarks: string;
}
