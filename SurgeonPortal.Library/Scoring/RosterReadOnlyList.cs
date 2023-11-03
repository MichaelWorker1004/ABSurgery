using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Scoring.RosterReadOnlyListFactory;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class RosterReadOnlyList : YtgReadOnlyListBase<IRosterReadOnlyList, IRosterReadOnly, int>, IRosterReadOnlyList
    {
        private readonly IRosterReadOnlyDal _rosterReadOnlyDal;

        public RosterReadOnlyList(
            IIdentityProvider identityProvider,
            IRosterReadOnlyDal rosterReadOnlyDal)
            : base(identityProvider)
        {
            _rosterReadOnlyDal = rosterReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(RosterReadOnlyList),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByExaminationHeaderId(GetByExaminationHeaderIdCriteria criteria)
        
        {
            var dtos = await _rosterReadOnlyDal.GetByExaminationHeaderIdAsync(
                _identity.GetUserId<int>(),
                criteria.ExamHeaderId);
        			
            FetchChildren(dtos);
        }
    }
}
