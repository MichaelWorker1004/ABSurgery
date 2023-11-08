using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseFeedbackReadOnly : IYtgReadOnlyBase<int>
    {
        int Id { get; }
        int UserId { get; }
        int CaseHeaderId { get; }
        string Feedback { get; }
    }
}
