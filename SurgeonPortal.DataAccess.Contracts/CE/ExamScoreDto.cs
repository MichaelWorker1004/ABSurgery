using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.CE
{
    public class ExamScoreDto : YtgBusinessBaseDto
    {
        public int ExamScheduleScoreId { get; set; }
        public int ExamScheduleId { get; set; }
        public int ExaminerUserId { get; set; }
        public int ExaminerScore { get; set; }
        public bool? Submitted { get; set; }
        public DateTime? SubmittedDateTimeUTC { get; set; }
    }
}
