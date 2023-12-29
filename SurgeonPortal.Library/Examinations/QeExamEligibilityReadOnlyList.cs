using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Examinations.QeExamEligibilityReadOnlyListFactory;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class QeExamEligibilityReadOnlyList : YtgReadOnlyListBase<IQeExamEligibilityReadOnlyList, IQeExamEligibilityReadOnly, int>, IQeExamEligibilityReadOnlyList
    {
        private readonly IQeExamEligibilityReadOnlyDal _qeExamEligibilityReadOnlyDal;

        public QeExamEligibilityReadOnlyList(
            IIdentityProvider identityProvider,
            IQeExamEligibilityReadOnlyDal qeExamEligibilityReadOnlyDal)
            : base(identityProvider)
        {
            _qeExamEligibilityReadOnlyDal = qeExamEligibilityReadOnlyDal;
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
        private async Task GetByUserId()
        
        {
            var dtos = await _qeExamEligibilityReadOnlyDal.GetByUserIdAsync(_identity.GetUserId<int>());
        			
            FetchChildren(dtos);
        }
    }
}
