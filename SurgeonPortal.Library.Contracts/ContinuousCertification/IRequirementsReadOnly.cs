using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IRequirementsReadOnly : IYtgReadOnlyBase<int>
    {
        string MeetingRequirements { get; }
    }
}
