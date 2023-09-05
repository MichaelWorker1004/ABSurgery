using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IUserProfessionalStanding : IYtgBusinessBase
    {
        int Id { get; set; }
        int UserId { get; set; }
        int? PrimaryPracticeId { get; set; }
        string PrimaryPractice { get; set; }
        int? OrganizationTypeId { get; set; }
        string OrganizationType { get; set; }
        string ExplanationOfNonPrivileges { get; set; }
        string ExplanationOfNonClinicalActivities { get; set; }
        int ClinicallyActive { get; set; }
    }
}
