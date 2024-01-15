
export interface IUserAppointmentModel {
    apptId: number;
    userId: number;
    practiceTypeId: number;
    practiceType: string;
    appointmentTypeId: number;
    primaryAppointment: boolean;
    appointmentType: string;
    organizationTypeId: number;
    authorizingOfficial: string;
    organizationType: string;
    organizationId: number;
    stateCode: string;
    other: string;
    organizationName: string;
}
