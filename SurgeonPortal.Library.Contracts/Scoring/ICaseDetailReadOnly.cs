using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseDetailReadOnly : IYtgReadOnlyBase
    {
        string CaseNumber { get; }
        string Description { get; }
        string Heading { get; }
        string content { get; }
        string Comments { get; }
        int? CaseCommentId { get; }
        int? SectionNumber { get; }
    }
}
