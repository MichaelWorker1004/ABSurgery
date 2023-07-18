namespace SurgeonPortal.DataAccess.Contracts.Scoring.CE
{
    public class ExamineeReadOnlyDto
    {
        public int ExamScheduleId { get; set; }
        public string FullName { get; set; }
        public string ExamDate { get; set; }
        public int ExamineeUserId { get; set; }
        public int ExamScoringId { get; set; }
    }
}
