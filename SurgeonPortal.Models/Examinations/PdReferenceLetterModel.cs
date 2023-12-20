using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Examinations
{
    public class PdReferenceLetterModel
    {
        public decimal Id { get; set; }
        public int? UserId { get; set; }
        public string Hosp { get; set; }
        public string Official { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public DateTime? LetterSent { get; set; }
        public int Status { get; set; }
        public int ExamId { get; set; }
        public string FullName { get; set; }
    }
}
