using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Examinations
{
    public class InsertUserExamCommandModel
    {
        public int UserId { get; set; }
        public int ExamHeaderId { get; set; }
    }
}
