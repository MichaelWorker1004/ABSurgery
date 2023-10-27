using System.Collections.Generic;

namespace SurgeonPortal.Models.Scoring.CE
{
    public class TitleReadOnlyModel
    {
        public string Title { get; set; }
        public int CaseHeaderId { get; set; }
        public int? ExamCaseId { get; set; }
        public List<CaseDetailReadOnlyModel> Sections { get; set; }
        public int? Score { get; set; }
        public bool? CriticalFail { get; set; }
        public string Remarks { get; set; }
    }
}
