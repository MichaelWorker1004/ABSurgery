using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Users
{
    public class UserLoginModel
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}
