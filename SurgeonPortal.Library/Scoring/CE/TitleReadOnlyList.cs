using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring.CE;
using SurgeonPortal.Library.Contracts.Scoring.CE;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Scoring.CE.TitleReadOnlyListFactory;

namespace SurgeonPortal.Library.Scoring.CE
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class TitleReadOnlyList : YtgReadOnlyListBase<ITitleReadOnlyList, ITitleReadOnly>, ITitleReadOnlyList
    {
        private readonly ITitleReadOnlyDal _titleReadOnlyDal;

        public TitleReadOnlyList(ITitleReadOnlyDal titleReadOnlyDal)
        {
            _titleReadOnlyDal = titleReadOnlyDal;
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
        private async Task GetById(GetByIdCriteria criteria)
        
        {
            var dtos = await _titleReadOnlyDal.GetByIdAsync(criteria.ExamScheduleId);
        			
            FetchChildren(dtos);
        }
    }
}
