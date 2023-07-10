using System;

namespace SurgeonPortal.Models.Scoring
{
    public class DashboardRosterReadOnlyModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? SessionNumber { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
