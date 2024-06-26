using System;
using System.Collections.Generic;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public class AppUserReadOnlyDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public bool ResetRequired { get; set; }
        public DateTime? LastLoginDateUtc { get; set; }
        public List<UserClaimReadOnlyDto> Claims { get; set; }
    }
}
