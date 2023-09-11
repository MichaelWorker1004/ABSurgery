using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Users
{
    public class PasswordResetCommandModel
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public bool? PasswordReset { get; set; }
    }
}
