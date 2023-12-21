using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Users
{
    public class ResetForgotPasswordCommandModel
    {
        public Guid ResetGUID { get; set; }
        public string NewPassword { get; set; }
        public bool? Result { get; set; }
    }
}
