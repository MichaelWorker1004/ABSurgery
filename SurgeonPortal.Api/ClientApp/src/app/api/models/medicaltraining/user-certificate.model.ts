
export interface IUserCertificateModel {
    certificateId: number;
    userId: number;
    documentId: number;
    certificateTypeId: number;
    certificateType: string;
    issueDate: string;
    certificateNumber: string;
    file: File;
    createdByUserId: number;
    createdAtUtc: string;
    lastUpdatedAtUtc: string;
    lastUpdatedByUserId: number;
    [key: string]: any;
}
