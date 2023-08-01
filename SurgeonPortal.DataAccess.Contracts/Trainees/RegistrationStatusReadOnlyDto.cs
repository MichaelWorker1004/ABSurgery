using System;

namespace SurgeonPortal.DataAccess.Contracts.Trainees
{
    public class RegistrationStatusReadOnlyDto
    {
        public DateTime? RegOpenDate { get; set; }
        public DateTime? RegEndDate { get; set; }
        public int IsRegOpen { get; set; }
        public DateTime? RegLateDate { get; set; }
        public int IsRegLate { get; set; }
    }
}
