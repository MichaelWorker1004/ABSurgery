using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurgeonPortal.Shared
{
    public static class ApplicationClaims
    {
        public static readonly string UserId = "UserId";
        public static readonly string FullName = "FullName";
        public static readonly string EmailAddress = "EmailAddress";
        public static readonly string User = "User";
        public static readonly string Surgeon = "Surgeon";
        public static readonly string Trainee = "Trainee";
        public static readonly string Examiner = "Examiner";
    }
}
