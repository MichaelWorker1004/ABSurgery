
export interface IUserProfileModel {
    userProfileId: number;
    userId: number;
    firstName: string;
    middleName: string;
    lastName: string;
    suffix: string;
    displayName: string;
    officePhoneNumber: string;
    mobilePhoneNumber: string;
    birthCity: string;
    birthState: string;
    birthCountry: string;
    countryCitizenship: string;
    absId: string;
    certificationStatus: string;
    nPI: string;
    genderId: number;
    birthDate: string;
    race: string;
    ethnicity: string;
    firstLanguageId: number;
    bestLanguageId: number;
    receiveComms: boolean;
    userConfirmed: boolean;
    userConfirmedDate: string;
    createdByUserId: number;
    createdAtUtc: string;
    lastUpdatedAtUtc: string;
    lastUpdatedByUserId: number;
    street1: string;
    street2: string;
    city: string;
    state: string;
    zipCode: string;
    country: string;
}
