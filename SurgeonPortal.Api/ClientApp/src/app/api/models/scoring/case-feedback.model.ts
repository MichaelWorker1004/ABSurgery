
export interface ICaseFeedbackModel {
    id: number;
    userId: number;
    caseHeaderId: number;
    feedback: string;
    createdByUserId: number;
    lastUpdatedByUserId: number;
}
