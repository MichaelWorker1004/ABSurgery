using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Scoring.RosterReadOnlyListFactory;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class RosterReadOnlyList : YtgReadOnlyListBase<IRosterReadOnlyList, IRosterReadOnly>, IRosterReadOnlyList
    {
        private readonly IRosterReadOnlyDal _rosterReadOnlyDal;

        public RosterReadOnlyList(IRosterReadOnlyDal rosterReadOnlyDal)
        {
            _rosterReadOnlyDal = rosterReadOnlyDal;
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
        private async Task GetByExaminationHeaderId(GetByExaminationHeaderIdCriteria criteria)
        
        {
            var dtos = await _rosterReadOnlyDal.GetByExaminationHeaderIdAsync(criteria.ExamHeaderId);
        			
            FetchChildren(dtos);
        }
    }
}