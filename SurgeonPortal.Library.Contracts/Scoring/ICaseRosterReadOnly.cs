using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseRosterReadOnly : IYtgReadOnlyBase<int>
    {
        string CaseNumber { get; }
        string Description { get; }
        string Title { get; }
        int Id { get; }
    }
}
