using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public class UserCredentialDto : YtgBusinessBaseDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
