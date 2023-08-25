import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserAppointmentModel } from '../../models/professionalstanding/user-appointment.model';
import { IUserAppointmentReadOnlyModel } from '../../models/professionalstanding/user-appointment-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class UserAppointmentService {

    constructor(private apiService: ApiService) {}

 
        public deleteUserAppointment(apptId: number,
        apiVersion = '1.0'): Observable<any> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * apptId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [del_userhospappt]
            */
            
            
            return this.apiService.delete<IUserAppointmentModel>(`api/professional-standing/user-appointment?api-version=${apiVersion}&apptId=${apptId}`);
        }
 
        public retrieveUserAppointment_GetById(apptId: number,
        apiVersion = '1.0'): Observable<IUserAppointmentModel> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * apptId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_userhospappts_byid]
            */
            
            
            return this.apiService.get<IUserAppointmentModel>(`api/professional-standing/user-appointment/by-id?api-version=${apiVersion}&apptId=${apptId}`);
        }
 
        public createUserAppointment(model: IUserAppointmentModel, 
            apiVersion = '1.0'): Observable<IUserAppointmentModel> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * practiceTypeId:Number
            * appointmentTypeId:Number
            * organizationTypeId:Number
            * stateCode:String
            * organizationId:Number
            * authorizingOfficial:String
            * other:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [insert_userhospappt]
            */
            
            
            return this.apiService.post<IUserAppointmentModel>(`api/professional-standing/user-appointment?api-version=${apiVersion}`, 
                model);
        }
 
        public updateUserAppointment(apptId: number,
        model: IUserAppointmentModel,
        apiVersion = '1.0') : Observable<IUserAppointmentModel> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * apptId:Number
            * practiceTypeId:Number
            * appointmentTypeId:Number
            * organizationTypeId:Number
            * stateCode:String
            * organizationId:Number
            * authorizingOfficial:String
            * other:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_userhospappt]
            */
            
            
            
            return this.apiService.put<IUserAppointmentModel>(`api/professional-standing/user-appointment?api-version=${apiVersion}&apptId=${apptId}`,
            model);
        }
 
        public retrieveUserAppointmentReadOnly_GetByUserId(apiVersion = '1.0'): Observable<IUserAppointmentReadOnlyModel[]> {
            /**
            * Claims
            * SurgeonPortalClaims.SurgeonClaim
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_userhospappts_byuserid]
            */
            
            
            return this.apiService.get<IUserAppointmentReadOnlyModel[]>(`api/professional-standing/user-appointment/by-userid?api-version=${apiVersion}`);
        }


}
