using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Trainees
{
    public class ProgramReadOnlyDto : YtgBusinessBaseDto
    {
        public string ProgramName { get; set; }
        public string ProgramDirector { get; set; }
        public string ProgramNumber { get; set; }
        public string Exam { get; set; }
        public string ClinicalLevel { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
