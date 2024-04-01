using Csla;
using SurgeonPortal.DataAccess.Contracts.Billing;
using SurgeonPortal.Library.Contracts.Billing;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Exceptions;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Billing.ApplicationFeeReadOnlyFactory;

namespace SurgeonPortal.Library.Billing
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class ApplicationFeeReadOnly : YtgReadOnlyBase<ApplicationFeeReadOnly, int>, IApplicationFeeReadOnly
    {
        private readonly IApplicationFeeReadOnlyDal _applicationFeeReadOnlyDal;


        public ApplicationFeeReadOnly(
            IIdentityProvider identityProvider,
            IApplicationFeeReadOnlyDal applicationFeeReadOnlyDal)
            : base(identityProvider)
        {
            _applicationFeeReadOnlyDal = applicationFeeReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(SubTotal))]
        public decimal? SubTotal => ReadProperty(SubTotalProperty);
		public static readonly PropertyInfo<decimal?> SubTotalProperty = RegisterProperty<decimal?>(c => c.SubTotal);

        [DataMember]
		[DisplayName(nameof(InvoiceNumber))]
        public string InvoiceNumber => ReadProperty(InvoiceNumberProperty);
		public static readonly PropertyInfo<string> InvoiceNumberProperty = RegisterProperty<string>(c => c.InvoiceNumber);

        [DataMember]
		[DisplayName(nameof(PaidTotal))]
        public decimal? PaidTotal => ReadProperty(PaidTotalProperty);
		public static readonly PropertyInfo<decimal?> PaidTotalProperty = RegisterProperty<decimal?>(c => c.PaidTotal);

        [DataMember]
		[DisplayName(nameof(BalanceDue))]
        public decimal? BalanceDue => ReadProperty(BalanceDueProperty);
		public static readonly PropertyInfo<decimal?> BalanceDueProperty = RegisterProperty<decimal?>(c => c.BalanceDue);

        [DataMember]
		[DisplayName(nameof(TrackCode))]
        public string TrackCode => ReadProperty(TrackCodeProperty);
		public static readonly PropertyInfo<string> TrackCodeProperty = RegisterProperty<string>(c => c.TrackCode);

        [DataMember]
		[DisplayName(nameof(PaymentDate))]
        public DateTime? PaymentDate => ReadProperty(PaymentDateProperty);
		public static readonly PropertyInfo<DateTime?> PaymentDateProperty = RegisterProperty<DateTime?>(c => c.PaymentDate);


        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        // [ObjectAuthorizationRules]
        // public static void AddObjectAuthorizationRules()
        // {
        //     Csla.Rules.BusinessRules.AddRule(typeof(ApplicationFeeReadOnly),
        //         new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
        //             SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
        // }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByExamId(GetByExamIdCriteria criteria)
        
        {
            var dto = await _applicationFeeReadOnlyDal.GetByExamIdAsync(
                _identity.GetUserId<int>(),
                criteria.ExamId);
            
            if (dto == null)
            {
                throw new DataNotFoundException("ApplicationFeeReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(ApplicationFeeReadOnlyDto dto)
		{
            LoadProperty(SubTotalProperty, dto.SubTotal);
            LoadProperty(InvoiceNumberProperty, dto.InvoiceNumber);
            LoadProperty(PaidTotalProperty, dto.PaidTotal);
            LoadProperty(BalanceDueProperty, dto.BalanceDue);
            LoadProperty(TrackCodeProperty, dto.TrackCode);
            LoadProperty(PaymentDateProperty, dto.PaymentDate);
		} 
        
    }
}
