
export interface ICaseScoreModel {
    examScoringId: number;
    examCaseId: number;
    examinerUserId: number;
    examineeUserId: number;
    examineeFirstName: string;
    examineeMiddleName: string;
    examineeLastName: string;
    examineeSuffix: string;
    score: number;
    criticalFail: boolean;
    remarks: string;
    createdByUserId: number;
    createdAtUtc: string;
    lastUpdatedAtUtc: string;
    lastUpdatedByUserId: number;
}
