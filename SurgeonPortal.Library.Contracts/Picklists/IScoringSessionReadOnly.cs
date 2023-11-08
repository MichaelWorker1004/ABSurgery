using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IScoringSessionReadOnly : IYtgReadOnlyBase<int>
    {
        string ExamSchedule { get; }
        int Session1Id { get; }
        int? Session2Id { get; }
    }
}
