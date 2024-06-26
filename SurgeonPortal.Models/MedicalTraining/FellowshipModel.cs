using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.MedicalTraining
{
    public class FellowshipModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProgramName { get; set; }
        public string CompletionYear { get; set; }
        [MaxLength(8000, ErrorMessage = "The ProgramOther cannot be more than 8000 characters")]
        public string ProgramOther { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}
