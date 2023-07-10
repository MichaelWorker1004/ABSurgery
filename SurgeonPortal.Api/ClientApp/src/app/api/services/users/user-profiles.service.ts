import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserProfileModel } from '../../models/users/user-profile.model';
import { ApiService } from 'ytg-angular';

@Injectable({
  providedIn: 'root',
})
export class UserProfilesService {

    constructor(private apiService: ApiService) {}

 
        public retrieveUserProfile_GetByUserId(userId: number,
        apiVersion = '1.0'): Observable<IUserProfileModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * Business rules for property: FirstName
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: LastName
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: DisplayName
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: OfficePhoneNumber
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BirthCity
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BirthState
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BirthCountry
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: CountryCitizenship
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: GenderId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BirthDate
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Race
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Ethnicity
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: FirstLanguageId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BestLanguageId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: ReceiveComms
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Street1
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: City
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: State
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: ZipCode
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Country
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * userId:Number
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [get_user_profile_byuserid]
            */
            
            
            return this.apiService.get<IUserProfileModel>(`api/users/profiles/by-userId?api-version=${apiVersion}&userId=${userId}`);
        }
 
        public createUserProfile(model: IUserProfileModel, 
            apiVersion = '1.0'): Observable<IUserProfileModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * Business rules for property: FirstName
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: LastName
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: DisplayName
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: OfficePhoneNumber
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BirthCity
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BirthState
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BirthCountry
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: CountryCitizenship
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: GenderId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BirthDate
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Race
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Ethnicity
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: FirstLanguageId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BestLanguageId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: ReceiveComms
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Street1
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: City
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: State
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: ZipCode
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Country
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * firstName:String
            * middleName:String
            * lastName:String
            * suffix:String
            * displayName:String
            * officePhoneNumber:String
            * mobilePhoneNumber:String
            * birthCity:String
            * birthState:String
            * birthCountry:String
            * countryCitizenship:String
            * absId:String
            * nPI:String
            * genderId:Number
            * birthDate:String
            * race:String
            * ethnicity:String
            * firstLanguageId:Number
            * bestLanguageId:Number
            * receiveComms:Boolean
            * userConfirmed:Boolean
            * userConfirmedDate:String
            * street1:String
            * street2:String
            * city:String
            * state:String
            * zipCode:String
            * country:String
            * createdAtUtc:String
            * lastUpdatedAtUtc:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [insert_user_profile]
            */
            
            
            return this.apiService.post<IUserProfileModel>(`api/users/profiles?api-version=${apiVersion}`, 
                model);
        }
 
        public updateUserProfile(userId: number,
        model: IUserProfileModel,
        apiVersion = '1.0') : Observable<IUserProfileModel> {
            /**
            * Claims
            */
            
            /**
            * Business Rules
            * Business rules for property: FirstName
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: LastName
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: DisplayName
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: OfficePhoneNumber
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BirthCity
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BirthState
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BirthCountry
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: CountryCitizenship
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: GenderId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BirthDate
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Race
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Ethnicity
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: FirstLanguageId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: BestLanguageId
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: ReceiveComms
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Street1
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: City
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: State
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: ZipCode
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            * Business rules for property: Country
            *   Rule Name: Required
            *   Rule Value: YtgIm.Library.Rules.RuleOptions
            */
            
            /**
            * Required Parameters
            * userProfileId:Number
            * firstName:String
            * middleName:String
            * lastName:String
            * suffix:String
            * displayName:String
            * officePhoneNumber:String
            * mobilePhoneNumber:String
            * birthCity:String
            * birthState:String
            * birthCountry:String
            * countryCitizenship:String
            * absId:String
            * nPI:String
            * genderId:Number
            * birthDate:String
            * race:String
            * ethnicity:String
            * firstLanguageId:Number
            * bestLanguageId:Number
            * receiveComms:Boolean
            * userConfirmed:Boolean
            * userConfirmedDate:String
            * street1:String
            * street2:String
            * city:String
            * state:String
            * zipCode:String
            * country:String
            * createdAtUtc:String
            * lastUpdatedAtUtc:String
            * apiVersion
            */
            
            /**
            * Calls Sp(s)
            * [update_user_profile]
            */
            
            
            
            return this.apiService.put<IUserProfileModel>(`api/users/profiles?api-version=${apiVersion}&userId=${userId}`,
            model);
        }


}
