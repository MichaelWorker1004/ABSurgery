using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Examinations.ExamHistoryReadOnlyListFactory;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class ExamHistoryReadOnlyList : YtgReadOnlyListBase<IExamHistoryReadOnlyList, IExamHistoryReadOnly, int>, IExamHistoryReadOnlyList
    {
        private readonly IExamHistoryReadOnlyDal _examHistoryReadOnlyDal;

        public ExamHistoryReadOnlyList(
            IIdentityProvider identityProvider,
            IExamHistoryReadOnlyDal examHistoryReadOnlyDal)
            : base(identityProvider)
        {
            _examHistoryReadOnlyDal = examHistoryReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        // [ObjectAuthorizationRules]
        // public static void AddObjectAuthorizationRules()
        // {
        //     Csla.Rules.BusinessRules.AddRule(typeof(ExamHistoryReadOnlyList),
        //         new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
        //             SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
        // }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId()
        
        {
            var dtos = await _examHistoryReadOnlyDal.GetByUserIdAsync(_identity.GetUserId<int>());
        			
            FetchChildren(dtos);
        }
    }
}
