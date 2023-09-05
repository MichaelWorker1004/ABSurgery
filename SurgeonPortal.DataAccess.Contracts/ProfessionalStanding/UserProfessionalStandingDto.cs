using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public class UserProfessionalStandingDto : YtgBusinessBaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PrimaryPracticeId { get; set; }
        public string PrimaryPractice { get; set; }
        public int? OrganizationTypeId { get; set; }
        public string OrganizationType { get; set; }
        public string ExplanationOfNonPrivileges { get; set; }
        public string ExplanationOfNonClinicalActivities { get; set; }
        public int ClinicallyActive { get; set; }
    }
}
