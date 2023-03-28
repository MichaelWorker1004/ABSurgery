using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public class UserProfileDto : YtgBusinessBaseDto
    {
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string DisplayName { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string BirthCity { get; set; }
        public string BirthState { get; set; }
        public string BirthCountry { get; set; }
        public string CountryCitizenship { get; set; }
        public int? ABSId { get; set; }
        public int? CertificationStatusId { get; set; }
        public string NPI { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Race { get; set; }
        public string Ethnicity { get; set; }
        public string FirstLanguage { get; set; }
        public string BestLanguage { get; set; }
        public bool? ReceiveComms { get; set; }
        public bool? UserConfirmed { get; set; }
        public DateTime? UserConfirmedDate { get; set; }
    }
}
