using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IGetClinicallyActiveCommand : IYtgCommandBase<int>
    {
        int? UserId { get; }
        bool? ClinicallyActive { get; }
    }
}
