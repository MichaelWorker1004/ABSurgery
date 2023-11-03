using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IMedicalTraining : IYtgBusinessBase
    {
        int Id { get;  }
        int UserId { get;  }
        int? GraduateProfileId { get; set; }
        string GraduateProfileDescription { get; set; }
        string MedicalSchoolName { get; set; }
        string MedicalSchoolCity { get; set; }
        string MedicalSchoolStateId { get; set; }
        string MedicalSchoolStateName { get; set; }
        string MedicalSchoolCountryId { get; set; }
        string MedicalSchoolCountryName { get; set; }
        int? DegreeId { get; set; }
        string DegreeName { get; set; }
        string MedicalSchoolCompletionYear { get; set; }
        string ResidencyProgramName { get; set; }
        string ResidencyCompletionYear { get; set; }
        string ResidencyProgramOther { get; set; }
    }
}
