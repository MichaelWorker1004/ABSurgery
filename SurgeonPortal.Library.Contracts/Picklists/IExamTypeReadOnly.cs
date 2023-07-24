using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IExamTypeReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        string Code { get; }
    }
}
