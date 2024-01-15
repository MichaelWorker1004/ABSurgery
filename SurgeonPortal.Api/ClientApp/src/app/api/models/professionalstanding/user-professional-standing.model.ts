
export interface IUserProfessionalStandingModel {
    id: number;
    userId: number;
    explanationOfNonPrivileges: string;
    explanationOfNonClinicalActivities: string;
    clinicallyActive: number;
    createdByUserId: number;
    createdAtUtc: string;
    lastUpdatedAtUtc: string;
    lastUpdatedByUserId: number;
}
