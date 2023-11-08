using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuingMedicalEducation;
using SurgeonPortal.Library.Contracts.ContinuingMedicalEducation;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.ContinuingMedicalEducation.CmeAdjustmentReadOnlyListFactory;

namespace SurgeonPortal.Library.ContinuingMedicalEducation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class CmeAdjustmentReadOnlyList : YtgReadOnlyListBase<ICmeAdjustmentReadOnlyList, ICmeAdjustmentReadOnly, int>, ICmeAdjustmentReadOnlyList
    {
        private readonly ICmeAdjustmentReadOnlyDal _cmeAdjustmentReadOnlyDal;

        public CmeAdjustmentReadOnlyList(
            IIdentityProvider identityProvider,
            ICmeAdjustmentReadOnlyDal cmeAdjustmentReadOnlyDal)
            : base(identityProvider)
        {
            _cmeAdjustmentReadOnlyDal = cmeAdjustmentReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(CmeAdjustmentReadOnlyList),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId()
        
        {
            var dtos = await _cmeAdjustmentReadOnlyDal.GetByUserIdAsync(_identity.GetUserId<int>());
        			
            FetchChildren(dtos);
        }
    }
}
