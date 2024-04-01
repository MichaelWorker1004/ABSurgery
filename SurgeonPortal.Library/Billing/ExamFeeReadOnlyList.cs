using Csla;
using SurgeonPortal.DataAccess.Contracts.Billing;
using SurgeonPortal.Library.Contracts.Billing;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Billing.ExamFeeReadOnlyListFactory;

namespace SurgeonPortal.Library.Billing
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class ExamFeeReadOnlyList : YtgReadOnlyListBase<IExamFeeReadOnlyList, IExamFeeReadOnly, int>, IExamFeeReadOnlyList
    {
        private readonly IExamFeeReadOnlyDal _examFeeReadOnlyDal;

        public ExamFeeReadOnlyList(
            IIdentityProvider identityProvider,
            IExamFeeReadOnlyDal examFeeReadOnlyDal)
            : base(identityProvider)
        {
            _examFeeReadOnlyDal = examFeeReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        // [ObjectAuthorizationRules]
        // public static void AddObjectAuthorizationRules()
        // {
        //     Csla.Rules.BusinessRules.AddRule(typeof(ExamFeeReadOnlyList),
        //         new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
        //             SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
        // }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId()
        
        {
            var dtos = await _examFeeReadOnlyDal.GetByUserIdAsync(_identity.GetUserId<int>());
        			
            FetchChildren(dtos);
        }
    }
}
