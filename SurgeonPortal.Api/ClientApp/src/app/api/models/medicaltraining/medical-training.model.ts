
export interface IMedicalTrainingModel {
    id: number;
    userId: number;
    graduateProfileId: number;
    graduateProfileDescription: string;
    medicalSchoolName: string;
    medicalSchoolCity: string;
    medicalSchoolStateId: string;
    medicalSchoolStateName: string;
    medicalSchoolCountryId: string;
    medicalSchoolCountryName: string;
    degreeId: number;
    degreeName: string;
    medicalSchoolCompletionYear: string;
    residencyProgramName: string;
    residencyCompletionYear: string;
    residencyProgramOther: string;
    createdAtUtc: string;
    createdByUserId: number;
    lastUpdatedByUserId: number;
    lastUpdatedAtUtc: string;
}
