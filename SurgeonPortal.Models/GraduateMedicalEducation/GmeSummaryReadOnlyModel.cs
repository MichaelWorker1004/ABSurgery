using System;

namespace SurgeonPortal.Models.GraduateMedicalEducation
{
    public class GmeSummaryReadOnlyModel
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
