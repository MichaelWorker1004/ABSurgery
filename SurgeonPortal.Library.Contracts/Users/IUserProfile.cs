using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IUserProfile : IYtgBusinessBase
    {
        int UserProfileId { get; set; }
        int UserId { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Suffix { get; set; }
        string DisplayName { get; set; }
        string OfficePhoneNumber { get; set; }
        string MobilePhoneNumber { get; set; }
        string BirthCity { get; set; }
        string BirthState { get; set; }
        string BirthCountry { get; set; }
        string CountryCitizenship { get; set; }
        string AbsId { get;  }
        string CertificationStatus { get;  }
        string NPI { get;  }
        int? GenderId { get; set; }
        DateTime? BirthDate { get; set; }
        string Race { get; set; }
        string Ethnicity { get; set; }
        int? FirstLanguageId { get; set; }
        int? BestLanguageId { get; set; }
        bool? ReceiveComms { get; set; }
        bool? UserConfirmed { get; set; }
        DateTime? UserConfirmedDate { get; set; }
        string Street1 { get; set; }
        string Street2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string ZipCode { get; set; }
        string Country { get; set; }
    }
}
