using System.Collections.Generic;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public class AppUserReadOnlyDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string EmailAddress { get; set; }

        public List<UserClaimReadOnlyDto> Claims { get; set; }
    }
}
