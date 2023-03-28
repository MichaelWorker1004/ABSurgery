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
        int? ABSId { get; set; }
        int? CertificationStatusId { get; set; }
        string NPI { get; set; }
        string Gender { get; set; }
        DateTime? BirthDate { get; set; }
        string Race { get; set; }
        string Ethnicity { get; set; }
        string FirstLanguage { get; set; }
        string BestLanguage { get; set; }
        bool? ReceiveComms { get; set; }
        bool? UserConfirmed { get; set; }
        DateTime? UserConfirmedDate { get; set; }
    }
}
