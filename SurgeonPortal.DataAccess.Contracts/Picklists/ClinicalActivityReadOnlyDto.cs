namespace SurgeonPortal.DataAccess.Contracts.Picklists
{
    public class ClinicalActivityReadOnlyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCredit { get; set; }
        public bool IsEssential { get; set; }
    }
}
