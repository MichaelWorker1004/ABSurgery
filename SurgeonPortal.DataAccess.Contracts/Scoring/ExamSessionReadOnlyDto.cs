namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public class ExamSessionReadOnlyDto
    {
        public int ExamScheduleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string MeetingLink { get; set; }
        public bool? IsSubmitted { get; set; }
        public bool? IsCurrentSession { get; set; }
        public int? SessionNumber { get; set; }
    }
}
