using System;

namespace SurgeonPortal.Models.Examinations
{
    public class AdmissionCardAvailabilityReadOnlyModel
    {
        public bool AdmCardAvailable { get; set; }
        public DateTime? DatePosted { get; set; }
        public string ExamCode { get; set; }
        public string AdmCardReport { get; set; }
    }
}
