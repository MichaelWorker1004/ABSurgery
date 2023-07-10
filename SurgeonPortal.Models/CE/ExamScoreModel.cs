using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.CE
{
    public class ExamScoreModel
    {
        public int ExamScheduleScoreId { get; set; }
        public int ExamScheduleId { get; set; }
        public int ExaminerUserId { get; set; }
        public int ExaminerScore { get; set; }
        public bool? Submitted { get; set; }
        public DateTime? SubmittedDateTimeUTC { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int LastUpdatedByUserId { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
    }
}
