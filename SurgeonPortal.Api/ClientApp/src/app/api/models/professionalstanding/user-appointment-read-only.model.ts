
export interface IUserAppointmentReadOnlyModel {
    apptId: number;
    userId: number;
    practiceTypeId: number;
    practiceType: string;
    appointmentTypeId: number;
    appointmentType: string;
    organizationTypeId: number;
    authorizingOfficial: string;
    organizationType: string;
    organizationId: number;
    stateCode: string;
    other: string;
    organizationName: string;
}
