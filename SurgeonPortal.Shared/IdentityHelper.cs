using Csla;
using System.Security.Claims;

namespace SurgeonPortal.Shared
{
    public static class IdentityHelper
    {
        public static string UserName
        {
            get
            {
                var value = ApplicationContext.User.Identity.Name;

                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }

                return null;
            }
        }

        public static int UserId
        {
            get
            {
                try
                {
                    var claimsIdentity = (ApplicationContext.User.Identity as ClaimsIdentity);
                    var value = claimsIdentity.FindFirst(ApplicationClaims.UserId).Value;

                    if (int.TryParse(value, out int userId))
                    {
                        return userId;
                    }

                    return 0;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}