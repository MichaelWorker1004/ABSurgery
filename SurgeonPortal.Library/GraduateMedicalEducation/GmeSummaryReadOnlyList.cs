using Csla;
using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.GraduateMedicalEducation.GmeSummaryReadOnlyListFactory;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class GmeSummaryReadOnlyList : YtgReadOnlyListBase<IGmeSummaryReadOnlyList, IGmeSummaryReadOnly, int>, IGmeSummaryReadOnlyList
    {
        private readonly IGmeSummaryReadOnlyDal _gmeSummaryReadOnlyDal;

        public GmeSummaryReadOnlyList(
            IIdentityProvider identityProvider,
            IGmeSummaryReadOnlyDal gmeSummaryReadOnlyDal)
            : base(identityProvider)
        {
            _gmeSummaryReadOnlyDal = gmeSummaryReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(GmeSummaryReadOnlyList),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim));

        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId()
        
        {
            var dtos = await _gmeSummaryReadOnlyDal.GetByUserIdAsync(_identity.GetUserId<int>());
        			
            FetchChildren(dtos);
        }
    }
}
