using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Examinations
{
    public class ExamIntentionsModel
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? DateReceived { get; set; }
        public bool Intention { get; set; }
    }
}
