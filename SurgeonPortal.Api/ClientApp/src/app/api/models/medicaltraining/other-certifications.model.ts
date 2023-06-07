
export interface IOtherCertificationsModel {
    id: number;
    userId: number;
    certificateTypeId: number;
    certificateTypeName: string;
    issueDate: string;
    certificateNumber: string;
    createdByUserId: number;
    createdAtUtc: string;
    lastUpdatedAtUtc: string;
    lastUpdatedByUserId: number;
}
