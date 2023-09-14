using System;

namespace SurgeonPortal.Models.Users
{
    public class AppUserReadOnlyModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public bool ResetRequired { get; set; }
        public DateTime? LastLoginDateUtc { get; set; }
    }
}
