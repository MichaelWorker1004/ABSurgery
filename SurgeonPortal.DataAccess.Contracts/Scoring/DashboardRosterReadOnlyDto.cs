using System;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public class DashboardRosterReadOnlyDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? SessionNumber { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
