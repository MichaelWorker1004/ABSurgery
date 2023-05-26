
export interface IDocumentReadOnlyModel {
    id: number;
    userId: number;
    streamId: string;
    documentTypeId: number;
    documentName: string;
    documentType: string;
    internalViewOnly: boolean;
    uploadedBy: string;
    uploadedDateUtc: string;
}
