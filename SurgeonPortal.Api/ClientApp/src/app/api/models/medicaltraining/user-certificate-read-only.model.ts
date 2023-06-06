
export interface IUserCertificateReadOnlyModel {
    certificateId: number;
    userId: number;
    documentId: number;
    certificateTypeId: number;
    certificateType: string;
    issueDate: string;
    certificateNumber: string;
    documentName: string;
    uploadDateUtc: string;
}
