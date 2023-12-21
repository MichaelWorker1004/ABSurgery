using System;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public class VerifyForgotGuidCommandDto
    {
        public Guid ResetGUID { get; set; }
        public bool Result { get; set; }
    }
}
