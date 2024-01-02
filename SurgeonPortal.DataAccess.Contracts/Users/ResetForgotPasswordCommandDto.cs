using System;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public class ResetForgotPasswordCommandDto
    {
        public Guid ResetGUID { get; set; }
        public string NewPassword { get; set; }
        public bool? Result { get; set; }
    }
}
