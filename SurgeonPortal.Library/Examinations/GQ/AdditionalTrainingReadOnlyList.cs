using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations.GQ;
using SurgeonPortal.Library.Contracts.Examinations.GQ;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Examinations.GQ.AdditionalTrainingReadOnlyListFactory;

namespace SurgeonPortal.Library.Examinations.GQ
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class AdditionalTrainingReadOnlyList : YtgReadOnlyListBase<IAdditionalTrainingReadOnlyList, IAdditionalTrainingReadOnly>, IAdditionalTrainingReadOnlyList
    {
        private readonly IAdditionalTrainingReadOnlyDal _additionalTrainingReadOnlyDal;

        public AdditionalTrainingReadOnlyList(IAdditionalTrainingReadOnlyDal additionalTrainingReadOnlyDal)
        {
            _additionalTrainingReadOnlyDal = additionalTrainingReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(AdditionalTrainingReadOnlyList),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetAllByUserId()
        
        {
            var dtos = await _additionalTrainingReadOnlyDal.GetAllByUserIdAsync();
        			
            FetchChildren(dtos);
        }
    }
}
