using System;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public class QeExamEligibilityReadOnlyDto
    {
        public int? ExamId { get; set; }
        public string ExamCode { get; set; }
        public string ExamName { get; set; }
        public DateTime? AppOpenDate { get; set; }
        public DateTime? AppCloseDate { get; set; }
        public DateTime? AppLateDate { get; set; }
        public DateTime? AppDelayDate { get; set; }
        public DateTime? ExamStartDate { get; set; }
        public DateTime? ExamEndDate { get; set; }
        public int ApplicationApproved { get; set; }
        public int ExamRegistrationAvailable { get; set; }
        public int RegistrationOpen { get; set; }
    }
}
