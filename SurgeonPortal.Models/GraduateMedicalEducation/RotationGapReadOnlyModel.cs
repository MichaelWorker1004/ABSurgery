using System;

namespace SurgeonPortal.Models.GraduateMedicalEducation
{
    public class RotationGapReadOnlyModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PreviousRotationId { get; set; }
        public int? NextRotationId { get; set; }
    }
}
