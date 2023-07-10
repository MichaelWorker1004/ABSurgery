namespace SurgeonPortal.DataAccess.Contracts.Scoring.CE
{
    public class TitleReadOnlyDto
    {
        public string Title { get; set; }
        public int CaseHeaderId { get; set; }
        public int? ExamCaseId { get; set; }
    }
}
