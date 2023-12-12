
export interface IAccommodationModel {
    id: number;
    userId: number;
    accommodationID: number;
    accommodationName: string;
    documentId: number;
    documentName: string;
    examID: number;
    createdByUserId: number;
    createdAtUtc: string;
    lastUpdatedAtUtc: string;
    lastUpdatedByUserId: number;
}
