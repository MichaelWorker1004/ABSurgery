namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public class QeDashboardStatusReadOnlyDto
    {
        public string StatusType { get; set; }
        public int? Status { get; set; }
        public int Disabled { get; set; }
    }
}
