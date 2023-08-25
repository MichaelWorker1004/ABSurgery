using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Scoring.CaseScoreReadOnlyListFactory;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class CaseScoreReadOnlyList : YtgReadOnlyListBase<ICaseScoreReadOnlyList, ICaseScoreReadOnly>, ICaseScoreReadOnlyList
    {
        private readonly ICaseScoreReadOnlyDal _caseScoreReadOnlyDal;

        public CaseScoreReadOnlyList(ICaseScoreReadOnlyDal caseScoreReadOnlyDal)
        {
            _caseScoreReadOnlyDal = caseScoreReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(CaseScoreReadOnlyList),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));

        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByExamScheduleId(GetByExamScheduleIdCriteria criteria)
        
        {
            var dtos = await _caseScoreReadOnlyDal.GetByExamScheduleIdAsync(criteria.ExamScheduleId);
        			
            FetchChildren(dtos);
        }
    }
}
