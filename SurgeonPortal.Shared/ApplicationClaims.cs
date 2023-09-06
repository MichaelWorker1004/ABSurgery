using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurgeonPortal.Shared
{
    /// <summary>
    /// Names of user claims used by the application
    /// </summary>
    public static class ApplicationClaims
    {
        public const string UserId = "UserId";
        public const string FullName = "FullName";
        public const string EmailAddress = "EmailAddress";
        public const string User = "User";
        public const string Surgeon = "Surgeon";
        public const string Trainee = "Trainee";
        public const string Examiner = "Examiner";
    }
}
