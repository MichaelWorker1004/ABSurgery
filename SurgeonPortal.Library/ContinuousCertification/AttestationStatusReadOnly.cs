using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Exceptions;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.ContinuousCertification.AttestationStatusReadOnlyFactory;

namespace SurgeonPortal.Library.ContinuousCertification
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class AttestationStatusReadOnly : YtgReadOnlyBase<AttestationStatusReadOnly, int>, IAttestationStatusReadOnly
    {
        private readonly IAttestationStatusReadOnlyDal _attestationStatusReadOnlyDal;


        public AttestationStatusReadOnly(
            IIdentityProvider identityProvider,
            IAttestationStatusReadOnlyDal attestationStatusReadOnlyDal)
            : base(identityProvider)
        {
            _attestationStatusReadOnlyDal = attestationStatusReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(Status))]
        public int Status => ReadProperty(StatusProperty);
		public static readonly PropertyInfo<int> StatusProperty = RegisterProperty<int>(c => c.Status);


        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(AttestationStatusReadOnly),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId()
        
        {
            var dto = await _attestationStatusReadOnlyDal.GetByUserIdAsync(_identity.GetUserId<int>());
            
            if (dto == null)
            {
                throw new DataNotFoundException("AttestationStatusReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(AttestationStatusReadOnlyDto dto)
		{
            LoadProperty(StatusProperty, dto.Status);
		} 
        
    }
}
