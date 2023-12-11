using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.User
{
    public interface ICertificationStatusReadOnly : IYtgReadOnlyBase<int>
    {
        int? CertificationStatusId { get; }
        string Description { get; }
    }
}
