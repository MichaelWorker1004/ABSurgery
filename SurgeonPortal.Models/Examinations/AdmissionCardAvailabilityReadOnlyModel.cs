using System;

namespace SurgeonPortal.Models.Examinations
{
    public class AdmissionCardAvailabilityReadOnlyModel
    {
        public bool AdmCardAvailable { get; set; }
        public DateTime? DatePosted { get; set; }
        public int? ExamId { get; set; }
    }
}
