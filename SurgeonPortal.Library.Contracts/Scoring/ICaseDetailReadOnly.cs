using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseDetailReadOnly : IYtgReadOnlyBase
    {
        string CaseNumber { get; }
        int CaseContentId { get; }
        string Heading { get; }
        string Content { get; }
        string Comments { get; }
        int? CaseCommentId { get; }
        int? SectionNumber { get; }
    }
}
