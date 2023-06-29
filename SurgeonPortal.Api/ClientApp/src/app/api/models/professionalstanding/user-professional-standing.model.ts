
export interface IUserProfessionalStandingModel {
    id: number;
    userId: number;
    primaryPracticeId: number;
    primaryPractice: string;
    organizationTypeId: number;
    organizationType: string;
    explanationOfNonPrivileges: string;
    explanationOfNonClinicalActivities: string;
    createdByUserId: number;
    createdAtUtc: string;
    lastUpdatedAtUtc: string;
    lastUpdatedByUserId: number;
}