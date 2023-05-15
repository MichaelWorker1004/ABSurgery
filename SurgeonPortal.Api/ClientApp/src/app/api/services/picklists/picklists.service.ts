import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { IAccreditedProgramInstitutionReadOnlyModel } from '../../models/picklists/accredited-program-institution-read-only.model';
import { ICountryReadOnlyModel } from '../../models/picklists/country-read-only.model';
import { IEthnicityReadOnlyModel } from '../../models/picklists/ethnicity-read-only.model';
import { IGenderReadOnlyModel } from '../../models/picklists/gender-read-only.model';
import { ILanguageReadOnlyModel } from '../../models/picklists/language-read-only.model';
import { IRaceReadOnlyModel } from '../../models/picklists/race-read-only.model';
import { IStateReadOnlyModel } from '../../models/picklists/state-read-only.model';
import { ApiService } from "ytg-angular";
import { ITrainingTypeReadOnlyModel } from '../../models/picklists/training-type-read-only.model';

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
