using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Examinations.ExamOverviewReadOnlyListFactory;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class ExamOverviewReadOnlyList : YtgReadOnlyListBase<IExamOverviewReadOnlyList, IExamOverviewReadOnly>, IExamOverviewReadOnlyList
    {
        private readonly IExamOverviewReadOnlyDal _examOverviewReadOnlyDal;

        public ExamOverviewReadOnlyList(IExamOverviewReadOnlyDal examOverviewReadOnlyDal)
        {
            _examOverviewReadOnlyDal = examOverviewReadOnlyDal;
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
        private async Task GetAll()
        
        {
            var dtos = await _examOverviewReadOnlyDal.GetAllAsync();
        			
            FetchChildren(dtos);
        }
    }
}
