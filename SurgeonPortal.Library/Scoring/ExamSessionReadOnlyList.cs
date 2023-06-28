using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Scoring.ExamSessionReadOnlyListFactory;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class ExamSessionReadOnlyList : YtgReadOnlyListBase<IExamSessionReadOnlyList, IExamSessionReadOnly>, IExamSessionReadOnlyList
    {
        private readonly IExamSessionReadOnlyDal _examSessionReadOnlyDal;

        public ExamSessionReadOnlyList(IExamSessionReadOnlyDal examSessionReadOnlyDal)
        {
            _examSessionReadOnlyDal = examSessionReadOnlyDal;
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
        private async Task GetByUserId(GetByUserIdCriteria criteria)
        
        {
            var dtos = await _examSessionReadOnlyDal.GetByUserIdAsync(criteria.ExamDate);
        			
            FetchChildren(dtos);
        }
    }
}
