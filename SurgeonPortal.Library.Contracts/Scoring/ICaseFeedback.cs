using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseFeedback : IYtgBusinessBase
    {
        int Id { get; set; }
        int UserId { get; set; }
        int CaseContentId { get; set; }
        string Feedback { get; set; }
    }
}
