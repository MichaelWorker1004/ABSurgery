using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Examinations.QeAttestationReadOnlyListFactory;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class QeAttestationReadOnlyList : YtgReadOnlyListBase<IQeAttestationReadOnlyList, IQeAttestationReadOnly, int>, IQeAttestationReadOnlyList
    {
        private readonly IQeAttestationReadOnlyDal _qeAttestationReadOnlyDal;

        public QeAttestationReadOnlyList(
            IIdentityProvider identityProvider,
            IQeAttestationReadOnlyDal qeAttestationReadOnlyDal)
            : base(identityProvider)
        {
            _qeAttestationReadOnlyDal = qeAttestationReadOnlyDal;
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
            var dtos = await _qeAttestationReadOnlyDal.GetByExamIdAsync(
                _identity.GetUserId<int>(),
                criteria.ExamId);
        			
            FetchChildren(dtos);
        }
    }
}
