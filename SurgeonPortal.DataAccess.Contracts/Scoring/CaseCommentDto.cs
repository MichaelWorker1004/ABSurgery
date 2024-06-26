using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public class CaseCommentDto : YtgBusinessBaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CaseContentId { get; set; }
        public string Comments { get; set; }
    }
}
