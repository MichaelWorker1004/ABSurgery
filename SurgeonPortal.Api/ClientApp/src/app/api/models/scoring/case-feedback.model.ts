
export interface ICaseFeedbackModel {
    id: number;
    userId: number;
    caseContentId: number;
    feedback: string;
    createdByUserId: number;
    lastUpdatedByUserId: number;
}
