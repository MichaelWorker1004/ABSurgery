using Csla;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Picklists.ExamStatusReadOnlyListFactory;

namespace SurgeonPortal.Library.Picklists
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class ExamStatusReadOnlyList : YtgReadOnlyListBase<IExamStatusReadOnlyList, IExamStatusReadOnly>, IExamStatusReadOnlyList
    {
        private readonly IExamStatusReadOnlyDal _examStatusReadOnlyDal;

        public ExamStatusReadOnlyList(IExamStatusReadOnlyDal examStatusReadOnlyDal)
        {
            _examStatusReadOnlyDal = examStatusReadOnlyDal;
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
            var dtos = await _examStatusReadOnlyDal.GetAllAsync();
        			
            FetchChildren(dtos);
        }
    }
}
