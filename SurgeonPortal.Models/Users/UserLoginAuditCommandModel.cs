using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Users
{
    public class UserLoginAuditCommandModel
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public int ApplicationId { get; set; }
        public string LoginIpAddress { get; set; }
        public string LoginUserAgent { get; set; }
        public bool LoginSuccess { get; set; }
        public string LoginFailureReason { get; set; }
    }
}
