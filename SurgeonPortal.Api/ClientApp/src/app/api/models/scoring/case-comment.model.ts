
export interface ICaseCommentModel {
    id: number;
    userId: number;
    caseContentId: number;
    comments: string;
    createdByUserId: number;
    lastUpdatedByUserId: number;
}
