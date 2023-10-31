using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring.CE;
using SurgeonPortal.Library.Contracts.Scoring.CE;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Scoring.CE.TitleReadOnlyListFactory;

namespace SurgeonPortal.Library.Scoring.CE
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class TitleReadOnlyList : YtgReadOnlyListBase<ITitleReadOnlyList, ITitleReadOnly, int>, ITitleReadOnlyList
    {
        private readonly ITitleReadOnlyDal _titleReadOnlyDal;

        public TitleReadOnlyList(
            IIdentityProvider identityProvider,
            ITitleReadOnlyDal titleReadOnlyDal)
            : base(identityProvider)
        {
            _titleReadOnlyDal = titleReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(TitleReadOnlyList),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetById(GetByIdCriteria criteria)
        
        {
            var dtos = await _titleReadOnlyDal.GetByIdAsync(
                criteria.ExamScheduleId,
                criteria.ExamineeUserId);
        			
            FetchChildren(dtos);
        }
    }
}
