
export interface IAccommodationModel {
    id: number;
    userId: number;
    accommodationID: number;
    documentId: number;
    examID: number;
    createdByUserId: number;
    createdAtUtc: string;
    lastUpdatedAtUtc: string;
    lastUpdatedByUserId: number;
}
