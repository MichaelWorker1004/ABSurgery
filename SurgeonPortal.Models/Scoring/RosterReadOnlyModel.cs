namespace SurgeonPortal.Models.Scoring
{
    public class RosterReadOnlyModel
    {
        public int ExamScheduleId { get; set; }
        public int? DayNumber { get; set; }
        public int? SessionNumber { get; set; }
        public string Roster { get; set; }
        public string DisplayName { get; set; }
        public bool? IsSubmitted { get; set; }
    }
}
