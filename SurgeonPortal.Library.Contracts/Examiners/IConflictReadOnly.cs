using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examiners
{
    public interface IConflictReadOnly : IYtgReadOnlyBase<int>
    {
        int Id { get; }
        string DocumentName { get; }
    }
}
