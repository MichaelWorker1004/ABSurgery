using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IExamTypeReadOnly : IYtgReadOnlyBase<int>
    {
        int Id { get; }
        string Code { get; }
    }
}
