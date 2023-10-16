namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public class CaseFeedbackReadOnlyDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CaseHeaderId { get; set; }
        public string Feedback { get; set; }
    }
}
