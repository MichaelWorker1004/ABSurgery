using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Users
{
    public class UserCredentialModel
    {
        [RegularExpression(@"^$|^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "The email address must be a valid format")]
        public string EmailAddress { get; set; }
        [RegularExpression(@"^$|^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "The password does not meet the minimum requirements.  Passwords must be a minimum length of 8 characters, at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        public string Password { get; set; }
    }
}
