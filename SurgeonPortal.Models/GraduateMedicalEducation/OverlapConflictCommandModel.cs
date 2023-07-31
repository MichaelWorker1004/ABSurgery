using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.GraduateMedicalEducation
{
    public class OverlapConflictCommandModel
    {
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool OverlapConflict { get; set; }
        public int? RotationId { get; set; }
    }
}
