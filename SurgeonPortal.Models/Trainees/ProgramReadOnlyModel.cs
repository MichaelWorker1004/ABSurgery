using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Trainees
{
    public class ProgramReadOnlyModel
    {
        public string ProgramName { get; set; }
        public string ProgramDirector { get; set; }
        public string ProgramNumber { get; set; }
        public string Exam { get; set; }
        public string ClinicalLevel { get; set; }
    }
}
