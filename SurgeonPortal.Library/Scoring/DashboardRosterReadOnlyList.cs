using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Scoring.DashboardRosterReadOnlyListFactory;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class DashboardRosterReadOnlyList : YtgReadOnlyListBase<IDashboardRosterReadOnlyList, IDashboardRosterReadOnly, int>, IDashboardRosterReadOnlyList
    {
        private readonly IDashboardRosterReadOnlyDal _dashboardRosterReadOnlyDal;

        public DashboardRosterReadOnlyList(
            IIdentityProvider identityProvider,
            IDashboardRosterReadOnlyDal dashboardRosterReadOnlyDal)
            : base(identityProvider)
        {
            _dashboardRosterReadOnlyDal = dashboardRosterReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(DashboardRosterReadOnlyList),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));

        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId(GetByUserIdCriteria criteria)
        
        {
            var dtos = await _dashboardRosterReadOnlyDal.GetByUserIdAsync(
                _identity.GetUserId<int>(),
                criteria.ExamDate);
        			
            FetchChildren(dtos);
        }
    }
}
