using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuingMedicalEducation;
using SurgeonPortal.Library.Contracts.ContinuingMedicalEducation;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.ContinuingMedicalEducation.CmeCreditReadOnlyListFactory;

namespace SurgeonPortal.Library.ContinuingMedicalEducation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class CmeCreditReadOnlyList : YtgReadOnlyListBase<ICmeCreditReadOnlyList, ICmeCreditReadOnly>, ICmeCreditReadOnlyList
    {
        private readonly ICmeCreditReadOnlyDal _cmeCreditReadOnlyDal;

        public CmeCreditReadOnlyList(ICmeCreditReadOnlyDal cmeCreditReadOnlyDal)
        {
            _cmeCreditReadOnlyDal = cmeCreditReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(CmeCreditReadOnlyList),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId()
        
        {
            var dtos = await _cmeCreditReadOnlyDal.GetByUserIdAsync(_identity.GetUserId<int>());
        			
            FetchChildren(dtos);
        }
    }
}
