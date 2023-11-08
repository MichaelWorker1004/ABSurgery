using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuingMedicalEducation;
using SurgeonPortal.Library.Contracts.ContinuingMedicalEducation;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.ContinuingMedicalEducation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class CmeAdjustmentReadOnly : YtgReadOnlyBase<CmeAdjustmentReadOnly, int>, ICmeAdjustmentReadOnly
    {        


        public CmeAdjustmentReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
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
		[DisplayName(nameof(IssuedBy))]
        public string IssuedBy => ReadProperty(IssuedByProperty);
		public static readonly PropertyInfo<string> IssuedByProperty = RegisterProperty<string>(c => c.IssuedBy);

        [DataMember]
		[DisplayName(nameof(CreditExpDate))]
        public DateTime CreditExpDate => ReadProperty(CreditExpDateProperty);
		public static readonly PropertyInfo<DateTime> CreditExpDateProperty = RegisterProperty<DateTime>(c => c.CreditExpDate);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(CmeAdjustmentReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(CmeAdjustmentReadOnlyDto dto)
		{
            LoadProperty(CmeIdProperty, dto.CmeId);
            LoadProperty(UserIdProperty, dto.UserId);
            LoadProperty(DateProperty, dto.Date);
            LoadProperty(DescriptionProperty, dto.Description);
            LoadProperty(CreditsTotalProperty, dto.CreditsTotal);
            LoadProperty(CreditsSAProperty, dto.CreditsSA);
            LoadProperty(IssuedByProperty, dto.IssuedBy);
            LoadProperty(CreditExpDateProperty, dto.CreditExpDate);
		} 
        
    }
}
