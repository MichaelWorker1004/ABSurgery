
export interface IRotationModel {
    id: number;
    userId: number;
    startDate: string;
    endDate: string;
    clinicalLevelId: number;
    clinicalLevel: string;
    clinicalActivityId: number;
    programName: string;
    nonSurgicalActivity: string;
    alternateInstitutionName: string;
    isInternationalRotation: boolean;
    isEssential: boolean;
    isCredit: boolean;
    other: string;
    fourMonthRotationExplain: string;
    nonPrimaryExplain: string;
    nonClinicalExplain: string;
    createdByUserId: number;
    createdAtUtc: string;
    lastUpdatedAtUtc: string;
    lastUpdatedByUserId: number;
    clinicalActivity: string;
}
