
export interface IRotationReadOnlyModel {
    id: number;
    startDate: string;
    endDate: string;
    programName: string;
    alternateInstitutionName: string;
    clinicalLevelId: number;
    clinicalLevel: string;
    isEssential: boolean;
    isCredit: boolean;
    other: string;
    nonSurgicalActivity: string;
    isInternationalRotation: boolean;
    clinicalActivity: string;
}
