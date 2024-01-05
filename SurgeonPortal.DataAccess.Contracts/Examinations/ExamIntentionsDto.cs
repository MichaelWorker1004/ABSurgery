using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public class ExamIntentionsDto : YtgBusinessBaseDto
    {
        public int UserId { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? DateReceived { get; set; }
        public bool Intention { get; set; }
    }
}
