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
import { IFellowshipProgramReadOnlyModel } from '../../models/picklists/fellowship-program-read-only.model';
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
import { IStateReadOnlyModel } from '../../models/picklists/state-read-only.model';
import { ITrainingTypeReadOnlyModel } from '../../models/picklists/training-type-read-only.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class PicklistsService {
    private readonly baseEndpoint = 'api/picklists';

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
            
            
            return this.apiService.get<IAccreditedProgramInstitutionReadOnlyModel[]>(`${this.baseEndpoint}/accredited-program-institutions?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IAppointmentTypeReadOnlyModel[]>(`${this.baseEndpoint}/appointment-types?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<ICertificateTypeReadOnlyModel[]>(`${this.baseEndpoint}/certificate-types?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IClinicalActivityReadOnlyModel[]>(`${this.baseEndpoint}/clinical-activities?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IClinicalLevelReadOnlyModel[]>(`${this.baseEndpoint}/clinical-levels?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<ICountryReadOnlyModel[]>(`${this.baseEndpoint}/countries?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IDegreeReadOnlyModel[]>(`${this.baseEndpoint}/degrees?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IDocumentTypeReadOnlyModel[]>(`${this.baseEndpoint}/document-types?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IEthnicityReadOnlyModel[]>(`${this.baseEndpoint}/ethnicities?api-version=${apiVersion}`);
        }

        public retrieveFellowshipProgramReadOnly_GetAll(apiVersion = '1.0'): Observable<IFellowshipProgramReadOnlyModel[]> {
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
            * [get_fellowship_program]
            */
            
            
            return this.apiService.get<IFellowshipProgramReadOnlyModel[]>(`${this.baseEndpoint}/fellowship-programs?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IGenderReadOnlyModel[]>(`${this.baseEndpoint}/genders?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IGraduateProfileReadOnlyModel[]>(`${this.baseEndpoint}/graduate-profile?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IJcahoOrganizationReadOnlyModel[]>(`${this.baseEndpoint}/jcaho-organizations?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<ILanguageReadOnlyModel[]>(`${this.baseEndpoint}/languages?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<ILicenseTypeReadOnlyModel[]>(`${this.baseEndpoint}/license-types?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IOrganizationTypeReadOnlyModel[]>(`${this.baseEndpoint}/organization-types?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IPracticeTypeReadOnlyModel[]>(`${this.baseEndpoint}/practice-types?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IPrimaryPracticeReadOnlyModel[]>(`${this.baseEndpoint}/primary-practices?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IRaceReadOnlyModel[]>(`${this.baseEndpoint}/races?api-version=${apiVersion}`);
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
            
            
            return this.apiService.get<IResidencyProgramReadOnlyModel[]>(`${this.baseEndpoint}/residency-programs?api-version=${apiVersion}`);
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


            return this.apiService.get<IStateReadOnlyModel[]>(`${this.baseEndpoint}/states?api-version=${apiVersion}&countryCode=${countryCode}`);
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
            
            
            return this.apiService.get<ITrainingTypeReadOnlyModel[]>(`${this.baseEndpoint}/training-types?api-version=${apiVersion}`);
        }


}
