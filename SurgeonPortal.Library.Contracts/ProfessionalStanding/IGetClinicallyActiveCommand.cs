using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IGetClinicallyActiveCommand : IYtgCommandBase
    {
        int? UserId { get; }
        bool? ClinicallyActive { get; }
    }
}
