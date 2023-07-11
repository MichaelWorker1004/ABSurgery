using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuingMedicalEducation;
using SurgeonPortal.Library.Contracts.ContinuingMedicalEducation;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.ContinuingMedicalEducation.CmeAdjustmentReadOnlyListFactory;

namespace SurgeonPortal.Library.ContinuingMedicalEducation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class CmeAdjustmentReadOnlyList : YtgReadOnlyListBase<ICmeAdjustmentReadOnlyList, ICmeAdjustmentReadOnly>, ICmeAdjustmentReadOnlyList
    {
        private readonly ICmeAdjustmentReadOnlyDal _cmeAdjustmentReadOnlyDal;

        public CmeAdjustmentReadOnlyList(ICmeAdjustmentReadOnlyDal cmeAdjustmentReadOnlyDal)
        {
            _cmeAdjustmentReadOnlyDal = cmeAdjustmentReadOnlyDal;
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
            var dtos = await _cmeAdjustmentReadOnlyDal.GetByUserIdAsync();
        			
            FetchChildren(dtos);
        }
    }
}
