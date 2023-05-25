using System;

namespace SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation
{
    public class GmeSummaryReadOnlyDto
    {
        public string ClinicalLevel { get; set; }
        public DateTime? MinStartDate { get; set; }
        public DateTime? MaxStartDate { get; set; }
        public string ProgramName { get; set; }
        public int? ClinicalWeeks { get; set; }
        public int? NonClinicalWeeks { get; set; }
        public int? EssentialsWeeks { get; set; }
    }
}
