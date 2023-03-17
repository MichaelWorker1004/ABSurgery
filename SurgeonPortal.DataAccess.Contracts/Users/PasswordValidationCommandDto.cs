using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public class PasswordValidationCommandDto : YtgBusinessBaseDto
    {
        public bool? PasswordsMatch { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
    }
}
