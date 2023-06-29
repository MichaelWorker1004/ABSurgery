namespace SurgeonPortal.Models.Scoring
{
    public class RosterReadOnlyModel
    {
        public int? DayNumber { get; set; }
        public int? SessionNumber { get; set; }
        public string Roster { get; set; }
        public string DisplayName { get; set; }
        public string Score { get; set; }
        public string CriticalFail { get; set; }
        public string Submitted { get; set; }
    }
}
