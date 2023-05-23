namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public class FellowshipReadOnlyDto
    {
        public int Id { get; set; }
        public string ProgramName { get; set; }
        public string CompletionYear { get; set; }
        public string ProgramOther { get; set; }
    }
}
