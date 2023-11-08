using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IFellowshipTypeReadOnly : IYtgReadOnlyBase<int>
    {
        string FellowshipType { get; }
        string FellowshipTypeName { get; }
    }
}
