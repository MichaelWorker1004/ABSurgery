using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseDetailReadOnly : IYtgReadOnlyBase<int>
    {
        string CaseNumber { get; }
        string CaseTitle { get; }
        int CaseContentId { get; }
        string Heading { get; }
        string Content { get; }
        string Comments { get; }
        int? CaseCommentId { get; }
        int? SectionNumber { get; }
    }
}
