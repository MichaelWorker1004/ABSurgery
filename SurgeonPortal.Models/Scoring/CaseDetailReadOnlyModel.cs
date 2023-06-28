namespace SurgeonPortal.Models.Scoring
{
    public class CaseDetailReadOnlyModel
    {
        public string CaseNumber { get; set; }
        public string Description { get; set; }
        public string Heading { get; set; }
        public string content { get; set; }
        public string Comments { get; set; }
        public int? CaseCommentId { get; set; }
        public int? SectionNumber { get; set; }
    }
}
