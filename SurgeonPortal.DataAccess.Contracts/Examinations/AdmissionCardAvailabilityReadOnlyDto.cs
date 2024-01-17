using System;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public class AdmissionCardAvailabilityReadOnlyDto
    {
        public bool AdmCardAvailable { get; set; }
        public DateTime? DatePosted { get; set; }
        public string ExamCode { get; set; }
        public string AdmCardReport { get; set; }
    }
}
