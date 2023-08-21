namespace SurgeonPortal.Models.Scoring
{
    public class CaseDetailReadOnlyModel
    {
        public string CaseNumber { get; set; }
        public int? CaseContentId { get; set; }
        public string Description { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
        public string Comments { get; set; }
        public int? CaseCommentId { get; set; }
        public string Feedback { get; set; }
        public int? CaseFeedbackId { get; set; }
        public int? SectionNumber { get; set; }
    }
}
