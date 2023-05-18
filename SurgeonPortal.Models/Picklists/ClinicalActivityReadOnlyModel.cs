namespace SurgeonPortal.Models.Picklists
{
    public class ClinicalActivityReadOnlyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCredit { get; set; }
        public bool IsEssential { get; set; }
    }
}
