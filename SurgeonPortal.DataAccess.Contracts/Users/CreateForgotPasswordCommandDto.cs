using System;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public class CreateForgotPasswordCommandDto
    {
        public string UserName { get; set; }
        public Guid? ResetGUID { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
