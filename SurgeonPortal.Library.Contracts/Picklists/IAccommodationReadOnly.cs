using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IAccommodationReadOnly : IYtgReadOnlyBase<int>
    {
        int Id { get; }
        string Code { get; }
        string DocumentLink { get; }
    }
}
