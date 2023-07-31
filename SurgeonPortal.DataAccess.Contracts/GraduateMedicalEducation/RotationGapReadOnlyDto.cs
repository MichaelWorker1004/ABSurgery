using System;

namespace SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation
{
    public class RotationGapReadOnlyDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PreviousRotationId { get; set; }
        public int? NextRotationId { get; set; }
    }
}
