using System;

namespace SurgeonPortal.Models.Trainees
{
    public class RegistrationStatusReadOnlyModel
    {
        public DateTime? RegOpenDate { get; set; }
        public DateTime? RegEndDate { get; set; }
        public int isRegOpen { get; set; }
        public DateTime? RegLateDate { get; set; }
        public int isRegLate { get; set; }
    }
}
