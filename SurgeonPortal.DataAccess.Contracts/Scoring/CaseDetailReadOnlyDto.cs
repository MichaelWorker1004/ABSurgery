namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public class CaseDetailReadOnlyDto
    {
        public string CaseNumber { get; set; }
        public int CaseContentId { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
        public string Comments { get; set; }
        public int? CaseCommentId { get; set; }
        public int? SectionNumber { get; set; }
    }
}
