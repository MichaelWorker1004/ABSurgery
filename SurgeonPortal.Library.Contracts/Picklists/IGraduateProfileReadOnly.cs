using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IGraduateProfileReadOnly : IYtgReadOnlyBase<int>
    {
        string Type { get; }
        string Description { get; }
    }
}
