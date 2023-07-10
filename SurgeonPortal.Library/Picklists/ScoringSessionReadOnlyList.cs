using Csla;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Picklists.ScoringSessionReadOnlyListFactory;

namespace SurgeonPortal.Library.Picklists
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class ScoringSessionReadOnlyList : YtgReadOnlyListBase<IScoringSessionReadOnlyList, IScoringSessionReadOnly>, IScoringSessionReadOnlyList
    {
        private readonly IScoringSessionReadOnlyDal _scoringSessionReadOnlyDal;

        public ScoringSessionReadOnlyList(IScoringSessionReadOnlyDal scoringSessionReadOnlyDal)
        {
            _scoringSessionReadOnlyDal = scoringSessionReadOnlyDal;
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
        private async Task GetByKeys(GetByKeysCriteria criteria)
        
        {
            var dtos = await _scoringSessionReadOnlyDal.GetByKeysAsync(criteria.ExamHeaderId);
        			
            FetchChildren(dtos);
        }
    }
}
