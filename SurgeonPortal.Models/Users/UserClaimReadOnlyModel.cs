namespace SurgeonPortal.Models.Users
{
    public class UserClaimReadOnlyModel
    {
        public string ClaimName { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public int UserClaimId { get; set; }
        public int ApplicationClaimId { get; set; }
        public int UserId { get; set; }
    }
}
