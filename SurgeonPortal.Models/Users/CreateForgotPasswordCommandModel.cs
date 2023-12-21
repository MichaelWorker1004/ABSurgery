using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Users
{
    public class CreateForgotPasswordCommandModel
    {
        public string UserName { get; set; }
        public Guid ResetGUID { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
