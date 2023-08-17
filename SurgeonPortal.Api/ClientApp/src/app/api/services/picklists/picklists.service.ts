import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAccreditedProgramInstitutionReadOnlyModel } from '../../models/picklists/accredited-program-institution-read-only.model';
import { IAppointmentTypeReadOnlyModel } from '../../models/picklists/appointment-type-read-only.model';
import { ICertificateTypeReadOnlyModel } from '../../models/picklists/certificate-type-read-only.model';
import { IClinicalActivityReadOnlyModel } from '../../models/picklists/clinical-activity-read-only.model';
import { IClinicalLevelReadOnlyModel } from '../../models/picklists/clinical-level-read-only.model';
import { ICountryReadOnlyModel } from '../../models/picklists/country-read-only.model';
import { IDegreeReadOnlyModel } from '../../models/picklists/degree-read-only.model';
import { IDocumentTypeReadOnlyModel } from '../../models/picklists/document-type-read-only.model';
import { IEthnicityReadOnlyModel } from '../../models/picklists/ethnicity-read-only.model';
import { IExamSpecialtyReadOnlyModel } from '../../models/picklists/exam-specialty-read-only.model';
import { IExamStatusReadOnlyModel } from '../../models/picklists/exam-status-read-only.model';
import { IExamTypeReadOnlyModel } from '../../models/picklists/exam-type-read-only.model';
import { IFellowshipProgramReadOnlyModel } from '../../models/picklists/fellowship-program-read-only.model';
import { IFellowshipTypeReadOnlyModel } from '../../models/picklists/fellowship-type-read-only.model';
import { IGenderReadOnlyModel } from '../../models/picklists/gender-read-only.model';
import { IGraduateProfileReadOnlyModel } from '../../models/picklists/graduate-profile-read-only.model';
import { IJcahoOrganizationReadOnlyModel } from '../../models/picklists/jcaho-organization-read-only.model';
import { ILanguageReadOnlyModel } from '../../models/picklists/language-read-only.model';
import { ILicenseTypeReadOnlyModel } from '../../models/picklists/license-type-read-only.model';
import { IOrganizationTypeReadOnlyModel } from '../../models/picklists/organization-type-read-only.model';
import { IPracticeTypeReadOnlyModel } from '../../models/picklists/practice-type-read-only.model';
import { IPrimaryPracticeReadOnlyModel } from '../../models/picklists/primary-practice-read-only.model';
import { IRaceReadOnlyModel } from '../../models/picklists/race-read-only.model';
import { IResidencyProgramReadOnlyModel } from '../../models/picklists/residency-program-read-only.model';
import { IScoringSessionReadOnlyModel } from '../../models/picklists/scoring-session-read-only.model';
import { IStateReadOnlyModel } from '../../models/picklists/state-read-only.model';
import { ITrainingTypeReadOnlyModel } from '../../models/picklists/training-type-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class PicklistsService {

    constructor(private apiService: ApiService) {}

 
        public retrieveAccreditedProgramInstitutionReadOnly_GetAll(apiVersion = '1.0'): Observable<IAccreditedProgramInstitutionReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_accredited_program_institutions]
            */
            
            
            return this.apiService.get<IAccreditedProgramInstitutionReadOnlyModel[]>(`api/picklists/accredited-program-institutions?api-version=${apiVersion}`);
        }
 
        public retrieveAppointmentTypeReadOnly_GetAll(apiVersion = '1.0'): Observable<IAppointmentTypeReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_appointment_types]
            */
            
            
            return this.apiService.get<IAppointmentTypeReadOnlyModel[]>(`api/picklists/appointment-types?api-version=${apiVersion}`);
        }
 
        public retrieveCertificateTypeReadOnly_GetAll(apiVersion = '1.0'): Observable<ICertificateTypeReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_certificate_types]
            */
            
            
            return this.apiService.get<ICertificateTypeReadOnlyModel[]>(`api/picklists/certificate-types?api-version=${apiVersion}`);
        }
 
        public retrieveClinicalActivityReadOnly_GetAll(apiVersion = '1.0'): Observable<IClinicalActivityReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_clinicalactivity]
            */
            
            
            return this.apiService.get<IClinicalActivityReadOnlyModel[]>(`api/picklists/clinical-activities?api-version=${apiVersion}`);
        }
 
        public retrieveClinicalLevelReadOnly_GetAll(apiVersion = '1.0'): Observable<IClinicalLevelReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_clinicallevel]
            */
            
            
            return this.apiService.get<IClinicalLevelReadOnlyModel[]>(`api/picklists/clinical-levels?api-version=${apiVersion}`);
        }
 
        public retrieveCountryReadOnly_GetAll(apiVersion = '1.0'): Observable<ICountryReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_picklist_country_all]
            */
            
            
            return this.apiService.get<ICountryReadOnlyModel[]>(`api/picklists/countries?api-version=${apiVersion}`);
        }
 
        public retrieveDegreeReadOnly_GetAll(apiVersion = '1.0'): Observable<IDegreeReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_degree]
            */
            
            
            return this.apiService.get<IDegreeReadOnlyModel[]>(`api/picklists/degrees?api-version=${apiVersion}`);
        }
 
        public retrieveDocumentTypeReadOnly_GetAll(apiVersion = '1.0'): Observable<IDocumentTypeReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_document_types]
            */
            
            
            return this.apiService.get<IDocumentTypeReadOnlyModel[]>(`api/picklists/document-types?api-version=${apiVersion}`);
        }
 
        public retrieveEthnicityReadOnly_GetAll(apiVersion = '1.0'): Observable<IEthnicityReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_picklists_ethnicities_all]
            */
            
            
            return this.apiService.get<IEthnicityReadOnlyModel[]>(`api/picklists/ethnicities?api-version=${apiVersion}`);
        }
 
        public retrieveExamSpecialtyReadOnly_GetAll(apiVersion = '1.0'): Observable<IExamSpecialtyReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_examspecialties]
            */
            
            
            return this.apiService.get<IExamSpecialtyReadOnlyModel[]>(`api/picklists/exam-specialties?api-version=${apiVersion}`);
        }
 
        public retrieveExamStatusReadOnly_GetAll(apiVersion = '1.0'): Observable<IExamStatusReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_examstatus]
            */
            
            
            return this.apiService.get<IExamStatusReadOnlyModel[]>(`api/picklists/exam-statuses?api-version=${apiVersion}`);
        }
 
        public retrieveExamTypeReadOnly_GetAll(apiVersion = '1.0'): Observable<IExamTypeReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_examtypes]
            */
            
            
            return this.apiService.get<IExamTypeReadOnlyModel[]>(`api/picklists/exam-types?api-version=${apiVersion}`);
        }
 
        public retrieveFellowshipProgramReadOnly_GetAll(fellowshipType: string,
        apiVersion = '1.0'): Observable<IFellowshipProgramReadOnlyModel[]> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * fellowshipType:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_fellowship_program]
            */
            
            
            return this.apiService.get<IFellowshipProgramReadOnlyModel[]>(`api/picklists/fellowship-programs?api-version=${apiVersion}&fellowshipType=${fellowshipType}`);
        }
 
        public retrieveFellowshipTypeReadOnly_Get(apiVersion = '1.0'): Observable<IFellowshipTypeReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_fellowship_types]
            */
            
            
            return this.apiService.get<IFellowshipTypeReadOnlyModel[]>(`api/picklists/fellowship-types?api-version=${apiVersion}`);
        }
 
        public retrieveGenderReadOnly_GetAll(apiVersion = '1.0'): Observable<IGenderReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_picklist_genders_all]
            */
            
            
            return this.apiService.get<IGenderReadOnlyModel[]>(`api/picklists/genders?api-version=${apiVersion}`);
        }
 
        public retrieveGraduateProfileReadOnly_GetAll(apiVersion = '1.0'): Observable<IGraduateProfileReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_graduate_profile]
            */
            
            
            return this.apiService.get<IGraduateProfileReadOnlyModel[]>(`api/picklists/graduate-profile?api-version=${apiVersion}`);
        }
 
        public retrieveJcahoOrganizationReadOnly_GetAll(apiVersion = '1.0'): Observable<IJcahoOrganizationReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_jcaho]
            */
            
            
            return this.apiService.get<IJcahoOrganizationReadOnlyModel[]>(`api/picklists/jcaho-organizations?api-version=${apiVersion}`);
        }
 
        public retrieveLanguageReadOnly_GetAll(apiVersion = '1.0'): Observable<ILanguageReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_picklist_languages_all]
            */
            
            
            return this.apiService.get<ILanguageReadOnlyModel[]>(`api/picklists/languages?api-version=${apiVersion}`);
        }
 
        public retrieveLicenseTypeReadOnly_GetAll(apiVersion = '1.0'): Observable<ILicenseTypeReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_license_types]
            */
            
            
            return this.apiService.get<ILicenseTypeReadOnlyModel[]>(`api/picklists/license-types?api-version=${apiVersion}`);
        }
 
        public retrieveOrganizationTypeReadOnly_GetAll(apiVersion = '1.0'): Observable<IOrganizationTypeReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_organization_type]
            */
            
            
            return this.apiService.get<IOrganizationTypeReadOnlyModel[]>(`api/picklists/organization-types?api-version=${apiVersion}`);
        }
 
        public retrievePracticeTypeReadOnly_GetAll(apiVersion = '1.0'): Observable<IPracticeTypeReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_practice_types]
            */
            
            
            return this.apiService.get<IPracticeTypeReadOnlyModel[]>(`api/picklists/practice-types?api-version=${apiVersion}`);
        }
 
        public retrievePrimaryPracticeReadOnly_GetAll(apiVersion = '1.0'): Observable<IPrimaryPracticeReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_primary_practice]
            */
            
            
            return this.apiService.get<IPrimaryPracticeReadOnlyModel[]>(`api/picklists/primary-practices?api-version=${apiVersion}`);
        }
 
        public retrieveRaceReadOnly_GetAll(apiVersion = '1.0'): Observable<IRaceReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_picklist_races_all]
            */
            
            
            return this.apiService.get<IRaceReadOnlyModel[]>(`api/picklists/races?api-version=${apiVersion}`);
        }
 
        public retrieveResidencyProgramReadOnly_GetAll(apiVersion = '1.0'): Observable<IResidencyProgramReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_residency_program]
            */
            
            
            return this.apiService.get<IResidencyProgramReadOnlyModel[]>(`api/picklists/residency-programs?api-version=${apiVersion}`);
        }
 
        public retrieveScoringSessionReadOnly_GetByKeys(examHeaderId: number,
        apiVersion = '1.0'): Observable<IScoringSessionReadOnlyModel[]> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * examHeaderId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_day_session_picklist]
            */
            
            
            return this.apiService.get<IScoringSessionReadOnlyModel[]>(`api/picklists/examiner-sessions?api-version=${apiVersion}&examHeaderId=${examHeaderId}`);
        }
 
        public retrieveStateReadOnly_GetByCountry(countryCode: string,
        apiVersion = '1.0'): Observable<IStateReadOnlyModel[]> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * No business rules exist for this model
            */
            
            /**
            * Required Parameters
            * countryCode:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_picklist_states_bycountry]
            */
            
            
            return this.apiService.get<IStateReadOnlyModel[]>(`api/picklists/states?api-version=${apiVersion}&countryCode=${countryCode}`);
        }
 
        public retrieveTrainingTypeReadOnly_GetAll(apiVersion = '1.0'): Observable<ITrainingTypeReadOnlyModel[]> {
            /**
            * Claims
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
            * [get_training_type]
            */
            
            
            return this.apiService.get<ITrainingTypeReadOnlyModel[]>(`api/picklists/training-types?api-version=${apiVersion}`);
        }


}
