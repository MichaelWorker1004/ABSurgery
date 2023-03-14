using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Users
{
    public class UserTokenModel
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
