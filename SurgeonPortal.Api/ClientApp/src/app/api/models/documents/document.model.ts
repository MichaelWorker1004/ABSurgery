
export interface IDocumentModel {
    id: number;
    userId: number;
    streamId: string;
    documentTypeId: number;
    documentName: string;
    documentType: string;
    internalViewOnly: boolean;
    createdByUserId: number;
    uploadedBy: string;
    uploadedDateUtc: string;
    createdAtUtc: string;
    lastUpdatedAtUtc: string;
    lastUpdatedByUserId: number;
}
