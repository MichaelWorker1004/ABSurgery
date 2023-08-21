using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseDetailReadOnly : IYtgReadOnlyBase
    {
        string CaseNumber { get; }
        int? CaseContentId { get; }
        string Description { get; }
        string Heading { get; }
        string Content { get; }
        string Comments { get; }
        int? CaseCommentId { get; }
        string Feedback { get; }
        int? CaseFeedbackId { get; }
        int? SectionNumber { get; }
    }
}
