using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public class UserLoginAuditCommandDto : YtgBusinessBaseDto
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
