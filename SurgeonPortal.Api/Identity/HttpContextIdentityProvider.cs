using SurgeonPortal.DataAccess.Contracts.Identity;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Shared;
using System.Security.Claims;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Api.Identity
{
    internal class HttpContextIdentityProvider : IIdentityProvider, IAbsIdentityProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextIdentityProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region IAbsIdentityProvider Implementation

        public T? GetUserId<T>()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;

                var val = httpContext?.User.FindFirst(YtgClaimType.UserId)?.Value ?? default;

                return (T?)Convert.ChangeType(val, typeof(T?));
            }
            catch (Exception)
            {
                return default;
            }
        }

        public string GetUserName() => ClaimsPrincipal.Current?.FindFirst(ApplicationClaims.EmailAddress)?.Value ?? "Unknown";

        #endregion
    }
}
