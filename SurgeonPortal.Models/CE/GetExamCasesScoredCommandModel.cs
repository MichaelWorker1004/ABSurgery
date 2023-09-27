using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.CE
{
    public class GetExamCasesScoredCommandModel
    {
        public int ExamScheduleId { get; set; }
        public bool CasesScored { get; set; }
    }
}
