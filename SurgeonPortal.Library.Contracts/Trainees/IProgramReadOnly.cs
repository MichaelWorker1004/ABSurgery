using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Trainees
{
    public interface IProgramReadOnly : IYtgBusinessBase
    {
        string ProgramName { get; set; }
        string ProgramDirector { get; set; }
        string ProgramNumber { get; set; }
        string Exam { get; set; }
        string ClinicalLevel { get; set; }
    }
}
