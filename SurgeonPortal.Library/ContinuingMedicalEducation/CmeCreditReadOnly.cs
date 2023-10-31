using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuingMedicalEducation;
using SurgeonPortal.Library.Contracts.ContinuingMedicalEducation;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Exceptions;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.ContinuingMedicalEducation.CmeCreditReadOnlyFactory;

namespace SurgeonPortal.Library.ContinuingMedicalEducation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class CmeCreditReadOnly : YtgReadOnlyBase<CmeCreditReadOnly, int>, ICmeCreditReadOnly
    {
        private readonly ICmeCreditReadOnlyDal _cmeCreditReadOnlyDal;


        public CmeCreditReadOnly(
            IIdentityProvider identityProvider,
            ICmeCreditReadOnlyDal cmeCreditReadOnlyDal)
            : base(identityProvider)
        {
            _cmeCreditReadOnlyDal = cmeCreditReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(CmeId))]
        public decimal CmeId => ReadProperty(CmeIdProperty);
		public static readonly PropertyInfo<decimal> CmeIdProperty = RegisterProperty<decimal>(c => c.CmeId);

        [DataMember]
		[DisplayName(nameof(UserId))]
        public int? UserId => ReadProperty(UserIdProperty);
		public static readonly PropertyInfo<int?> UserIdProperty = RegisterProperty<int?>(c => c.UserId);

        [DataMember]
		[DisplayName(nameof(Date))]
        public string Date => ReadProperty(DateProperty);
		public static readonly PropertyInfo<string> DateProperty = RegisterProperty<string>(c => c.Date);

        [DataMember]
		[DisplayName(nameof(Description))]
        public string Description => ReadProperty(DescriptionProperty);
		public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);

        [DataMember]
		[DisplayName(nameof(CreditsTotal))]
        public decimal CreditsTotal => ReadProperty(CreditsTotalProperty);
		public static readonly PropertyInfo<decimal> CreditsTotalProperty = RegisterProperty<decimal>(c => c.CreditsTotal);

        [DataMember]
		[DisplayName(nameof(CreditsSA))]
        public decimal? CreditsSA => ReadProperty(CreditsSAProperty);
		public static readonly PropertyInfo<decimal?> CreditsSAProperty = RegisterProperty<decimal?>(c => c.CreditsSA);

        [DataMember]
		[DisplayName(nameof(CMEDirect))]
        public int CMEDirect => ReadProperty(CMEDirectProperty);
		public static readonly PropertyInfo<int> CMEDirectProperty = RegisterProperty<int>(c => c.CMEDirect);

        [DataMember]
		[DisplayName(nameof(CreditExpDate))]
        public DateTime CreditExpDate => ReadProperty(CreditExpDateProperty);
		public static readonly PropertyInfo<DateTime> CreditExpDateProperty = RegisterProperty<DateTime>(c => c.CreditExpDate);


        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(CmeCreditReadOnly),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
        }

        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(CmeCreditReadOnlyDto dto)
        {
            FetchData(dto);
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetById(GetByIdCriteria criteria)
        
        {
            var dto = await _cmeCreditReadOnlyDal.GetByIdAsync(criteria.CmeId);
            
            if (dto == null)
            {
                throw new DataNotFoundException("CmeCreditReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(CmeCreditReadOnlyDto dto)
		{
            LoadProperty(CmeIdProperty, dto.CmeId);
            LoadProperty(UserIdProperty, dto.UserId);
            LoadProperty(DateProperty, dto.Date);
            LoadProperty(DescriptionProperty, dto.Description);
            LoadProperty(CreditsTotalProperty, dto.CreditsTotal);
            LoadProperty(CreditsSAProperty, dto.CreditsSA);
            LoadProperty(CMEDirectProperty, dto.CMEDirect);
            LoadProperty(CreditExpDateProperty, dto.CreditExpDate);
		} 
        
    }
}
