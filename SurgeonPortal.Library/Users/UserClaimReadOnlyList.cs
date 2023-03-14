using Csla;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Users.UserClaimReadOnlyListFactory;

namespace SurgeonPortal.Library.Users
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class UserClaimReadOnlyList : YtgReadOnlyListBase<IUserClaimReadOnlyList, IUserClaimReadOnly>, IUserClaimReadOnlyList
    {
        private readonly IUserClaimReadOnlyDal _userClaimReadOnlyDal;

        public UserClaimReadOnlyList(IUserClaimReadOnlyDal userClaimReadOnlyDal)
        {
            _userClaimReadOnlyDal = userClaimReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
                    Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        [FetchChild]
        private void FetchChild(List<UserClaimReadOnlyDto> dtos)
        {
            FetchChildren(dtos);
        }
    }
}
