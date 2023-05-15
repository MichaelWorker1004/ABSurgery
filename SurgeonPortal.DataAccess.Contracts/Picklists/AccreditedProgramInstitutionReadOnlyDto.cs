namespace SurgeonPortal.DataAccess.Contracts.Picklists
{
    public class AccreditedProgramInstitutionReadOnlyDto
    {
        public int ProgramId { get; set; }
        public string InstitutionName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
