using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public class MedicalTrainingDto : YtgBusinessBaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GraduateProfileId { get; set; }
        public string GraduateProfileDescription { get; set; }
        public string MedicalSchoolName { get; set; }
        public string MedicalSchoolCity { get; set; }
        public string MedicalSchoolStateId { get; set; }
        public string MedicalSchoolStateName { get; set; }
        public string MedicalSchoolCountryId { get; set; }
        public string MedicalSchoolCountryName { get; set; }
        public int DegreeId { get; set; }
        public string DegreeName { get; set; }
        public string MedicalSchoolCompletionYear { get; set; }
        public string ResidencyProgramName { get; set; }
        public string ResidencyCompletionYear { get; set; }
        public string ResidencyProgramOther { get; set; }
    }
}
