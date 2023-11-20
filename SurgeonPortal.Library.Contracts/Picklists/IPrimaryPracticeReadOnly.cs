using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IPrimaryPracticeReadOnly : IYtgReadOnlyBase<int>
    {
        int Id { get; }
        string Practice { get; }
    }
}
