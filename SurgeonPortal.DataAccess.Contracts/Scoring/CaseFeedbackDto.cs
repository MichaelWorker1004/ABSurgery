using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public class CaseFeedbackDto : YtgBusinessBaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CaseHeaderId { get; set; }
        public string Feedback { get; set; }
    }
}
