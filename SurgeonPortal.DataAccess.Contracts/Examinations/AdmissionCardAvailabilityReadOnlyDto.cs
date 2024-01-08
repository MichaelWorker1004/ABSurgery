using System;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public class AdmissionCardAvailabilityReadOnlyDto
    {
        public bool AdmCardAvailable { get; set; }
        public DateTime? DatePosted { get; set; }
        public int? ExamId { get; set; }
    }
}
