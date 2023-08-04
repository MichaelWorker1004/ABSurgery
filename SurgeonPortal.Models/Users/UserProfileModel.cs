using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Users
{
    public class UserProfileModel
    {
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        public string Suffix { get; set; }
        [Required(ErrorMessage = "DisplayName is required")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "OfficePhoneNumber is required")]
        public string OfficePhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        [Required(ErrorMessage = "BirthCity is required")]
        public string BirthCity { get; set; }
        public string BirthState { get; set; }
        [Required(ErrorMessage = "BirthCountry is required")]
        public string BirthCountry { get; set; }
        [Required(ErrorMessage = "CountryCitizenship is required")]
        public string CountryCitizenship { get; set; }
        public string AbsId { get; set; }
        public string CertificationStatus { get; set; }
        public string NPI { get; set; }
        [Required(ErrorMessage = "GenderId is required")]
        public int? GenderId { get; set; }
        [Required(ErrorMessage = "BirthDate is required")]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "Race is required")]
        public string Race { get; set; }
        [Required(ErrorMessage = "Ethnicity is required")]
        public string Ethnicity { get; set; }
        [Required(ErrorMessage = "FirstLanguageId is required")]
        public int? FirstLanguageId { get; set; }
        [Required(ErrorMessage = "BestLanguageId is required")]
        public int? BestLanguageId { get; set; }
        [Required(ErrorMessage = "ReceiveComms is required")]
        public bool? ReceiveComms { get; set; }
        [Required(ErrorMessage = "UserConfirmed is required")]
        public bool? UserConfirmed { get; set; }
        public DateTime? UserConfirmedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
        public int LastUpdatedByUserId { get; set; }
        [Required(ErrorMessage = "Street1 is required")]
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        public string State { get; set; }
        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
    }
}
