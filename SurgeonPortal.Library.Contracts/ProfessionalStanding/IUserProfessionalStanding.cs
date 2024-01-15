using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IUserProfessionalStanding : IYtgBusinessBase
    {
        int Id { get; set; }
        int UserId { get; set; }
        string ExplanationOfNonPrivileges { get; set; }
        string ExplanationOfNonClinicalActivities { get; set; }
        int ClinicallyActive { get; set; }
    }
}
