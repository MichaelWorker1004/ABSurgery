using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation
{
    public class OverlapConflictCommandDto : YtgBusinessBaseDto
    {
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool OverlapConflict { get; set; }
    }
}
