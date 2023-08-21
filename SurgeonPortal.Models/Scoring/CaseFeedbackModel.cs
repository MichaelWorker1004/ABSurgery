using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Scoring
{
    public class CaseFeedbackModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CaseContentId { get; set; }
        public string Feedback { get; set; }
        public int CreatedByUserId { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}
