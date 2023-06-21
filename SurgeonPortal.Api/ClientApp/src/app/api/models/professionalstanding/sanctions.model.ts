
export interface ISanctionsModel {
    id: number;
    userId: number;
    hadDrugAlchoholTreatment: boolean;
    hadHospitalPrivilegesDenied: boolean;
    hadLicenseRestricted: boolean;
    hadHospitalPrivilegesRestricted: boolean;
    hadFelonyConviction: boolean;
    hadCensure: boolean;
    explanation: string;
    createdByUserId: number;
    createdAtUtc: string;
    lastUpdatedAtUtc: string;
    lastUpdatedByUserId: number;
}
