using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Users
{
    public class VerifyForgotGuidCommandModel
    {
        public Guid ResetGUID { get; set; }
        public bool Result { get; set; }
    }
}
