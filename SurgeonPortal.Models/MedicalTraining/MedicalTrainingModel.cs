using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.MedicalTraining
{
    public class MedicalTrainingModel
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "GraduateProfileId is required")]
        public int GraduateProfileId { get; set; }
        public string GraduateProfileDescription { get; set; }
        [Required(ErrorMessage = "MedicalSchoolName is required")]
        [MaxLength(30, ErrorMessage = "The MedicalSchoolName cannot be more than 30 characters")]
        public string MedicalSchoolName { get; set; }
        [Required(ErrorMessage = "MedicalSchoolCity is required")]
        [MaxLength(30, ErrorMessage = "The MedicalSchoolCity cannot be more than 30 characters")]
        public string MedicalSchoolCity { get; set; }
        public string MedicalSchoolStateId { get; set; }
        public string MedicalSchoolStateName { get; set; }
        [Required(ErrorMessage = "MedicalSchoolCountryId is required")]
        public string MedicalSchoolCountryId { get; set; }
        public string MedicalSchoolCountryName { get; set; }
        [Required(ErrorMessage = "DegreeId is required")]
        public int DegreeId { get; set; }
        public string DegreeName { get; set; }
        [Required(ErrorMessage = "MedicalSchoolCompletionYear is required")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "The medical school completion year doesn't match the required pattern (YYYY)")]
        public string MedicalSchoolCompletionYear { get; set; }
        [Required(ErrorMessage = "ResidencyProgramName is required")]
        public string ResidencyProgramName { get; set; }
        [Required(ErrorMessage = "ResidencyCompletionYear is required")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "The residency completion year doesn't match the required pattern (YYYY)")]
        public string ResidencyCompletionYear { get; set; }
        [Required(ErrorMessage = "ResidencyProgramOther is required")]
        public string ResidencyProgramOther { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserId { get; set; }
        public int LastUpdatedByUserId { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
    }
}
