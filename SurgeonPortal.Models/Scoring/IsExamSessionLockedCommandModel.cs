using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Scoring
{
    public class IsExamSessionLockedCommandModel
    {
        public int ExamCaseId { get; set; }
        public bool? IsLocked { get; set; }
    }
}
