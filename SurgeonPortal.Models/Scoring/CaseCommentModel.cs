using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Scoring
{
    public class CaseCommentModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CaseContentId { get; set; }
        public string Comments { get; set; }
        public int CreatedByUserId { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}
