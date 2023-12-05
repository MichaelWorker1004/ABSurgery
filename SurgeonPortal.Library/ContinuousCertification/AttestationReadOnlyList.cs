using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.ContinuousCertification.AttestationReadOnlyListFactory;

namespace SurgeonPortal.Library.ContinuousCertification
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class AttestationReadOnlyList : YtgReadOnlyListBase<IAttestationReadOnlyList, IAttestationReadOnly, int>, IAttestationReadOnlyList
    {
        private readonly IAttestationReadOnlyDal _attestationReadOnlyDal;

        public AttestationReadOnlyList(
            IIdentityProvider identityProvider,
            IAttestationReadOnlyDal attestationReadOnlyDal)
            : base(identityProvider)
        {
            _attestationReadOnlyDal = attestationReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(AttestationReadOnlyList),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetAllByUserId()
        
        {
            var dtos = await _attestationReadOnlyDal.GetAllByUserIdAsync(_identity.GetUserId<int>());
        			
            FetchChildren(dtos);
        }
    }
}
