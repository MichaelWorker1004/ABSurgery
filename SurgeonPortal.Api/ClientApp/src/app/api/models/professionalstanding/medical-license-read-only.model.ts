
export interface IMedicalLicenseReadOnlyModel {
    licenseId: number;
    userId: number;
    issuingStateId: string;
    issuingState: string;
    licenseNumber: string;
    licenseTypeId: number;
    licenseType: string;
    issueDate: string;
    expireDate: string;
    reportingOrganization: string;
}
