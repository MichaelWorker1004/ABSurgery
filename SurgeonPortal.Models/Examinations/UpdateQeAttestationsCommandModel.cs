using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Examinations
{
    public class UpdateQeAttestationsCommandModel
    {
        public int UserId { get; set; }
        public int ExamId { get; set; }
    }
}
