using System.Collections.Generic;

namespace SurgeonPortal.Models.Scoring.CE
{
    public class ExamineeReadOnlyModel
    {
        public int ExamScheduleId { get; set; }
        public string FullName { get; set; }
        public string ExamDate { get; set; }
        public List<TitleReadOnlyModel> Cases { get; set; }
    }
}
