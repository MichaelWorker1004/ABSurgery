using SurgeonPortal.DataAccess.Contracts.Identity;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ytg.Framework.Identity;

namespace SurgeonPortal.DataAccess.Identity
{
    public class AbsIdentityProvider : IIdentityProvider, IAbsIdentityProvider
    {
        #region IAbsIdentityProvider Implementation

        public T GetUserId<T>()
        {
            try
            {
                var val = ClaimsPrincipal.Current.FindFirst(YtgClaimType.UserId).Value;

                return (T)Convert.ChangeType(val, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public string GetUserName() => ClaimsPrincipal.Current.FindFirst(ApplicationClaims.EmailAddress)?.Value;

        #endregion
    }
}
