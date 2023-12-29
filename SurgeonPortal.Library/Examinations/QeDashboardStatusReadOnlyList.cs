using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Examinations.QeDashboardStatusReadOnlyListFactory;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class QeDashboardStatusReadOnlyList : YtgReadOnlyListBase<IQeDashboardStatusReadOnlyList, IQeDashboardStatusReadOnly, int>, IQeDashboardStatusReadOnlyList
    {
        private readonly IQeDashboardStatusReadOnlyDal _qeDashboardStatusReadOnlyDal;

        public QeDashboardStatusReadOnlyList(
            IIdentityProvider identityProvider,
            IQeDashboardStatusReadOnlyDal qeDashboardStatusReadOnlyDal)
            : base(identityProvider)
        {
            _qeDashboardStatusReadOnlyDal = qeDashboardStatusReadOnlyDal;
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
        private async Task GetByExamId(GetByExamIdCriteria criteria)
        
        {
            var dtos = await _qeDashboardStatusReadOnlyDal.GetByExamIdAsync(
                _identity.GetUserId<int>(),
                criteria.ExamheaderId);
        			
            FetchChildren(dtos);
        }
    }
}
