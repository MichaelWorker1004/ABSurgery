using Csla;
using SurgeonPortal.DataAccess.Contracts.User;
using SurgeonPortal.Library.Contracts.User;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Exceptions;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.User.CertificationStatusReadOnlyFactory;

namespace SurgeonPortal.Library.User
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class CertificationStatusReadOnly : YtgReadOnlyBase<CertificationStatusReadOnly, int>, ICertificationStatusReadOnly
    {
        private readonly ICertificationStatusReadOnlyDal _certificationStatusReadOnlyDal;


        public CertificationStatusReadOnly(
            IIdentityProvider identityProvider,
            ICertificationStatusReadOnlyDal certificationStatusReadOnlyDal)
            : base(identityProvider)
        {
            _certificationStatusReadOnlyDal = certificationStatusReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(CertificationStatusId))]
        public int? CertificationStatusId => ReadProperty(CertificationStatusIdProperty);
		public static readonly PropertyInfo<int?> CertificationStatusIdProperty = RegisterProperty<int?>(c => c.CertificationStatusId);

        [DataMember]
		[DisplayName(nameof(Description))]
        public string Description => ReadProperty(DescriptionProperty);
		public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);


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
            var dto = await _certificationStatusReadOnlyDal.GetByUserIdAsync(_identity.GetUserId<int>());
            
            if (dto == null)
            {
                throw new DataNotFoundException("CertificationStatusReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(CertificationStatusReadOnlyDto dto)
		{
            LoadProperty(CertificationStatusIdProperty, dto.CertificationStatusId);
            LoadProperty(DescriptionProperty, dto.Description);
		} 
        
    }
}
