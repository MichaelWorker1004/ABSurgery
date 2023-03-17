using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Users
{
    public class PasswordValidationCommandModel
    {
        public bool? PasswordsMatch { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
    }
}
