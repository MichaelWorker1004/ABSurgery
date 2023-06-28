using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Scoring.CaseRosterReadOnlyListFactory;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class CaseRosterReadOnlyList : YtgReadOnlyListBase<ICaseRosterReadOnlyList, ICaseRosterReadOnly>, ICaseRosterReadOnlyList
    {
        private readonly ICaseRosterReadOnlyDal _caseRosterReadOnlyDal;

        public CaseRosterReadOnlyList(ICaseRosterReadOnlyDal caseRosterReadOnlyDal)
        {
            _caseRosterReadOnlyDal = caseRosterReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            

        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByScheduleId(GetByScheduleIdCriteria criteria)
        
        {
            var dtos = await _caseRosterReadOnlyDal.GetByScheduleIdAsync(
                criteria.ScheduleId1,
                criteria.ScheduleId2);
        			
            FetchChildren(dtos);
        }
    }
}
