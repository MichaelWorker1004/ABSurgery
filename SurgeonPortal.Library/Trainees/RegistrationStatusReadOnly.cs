using Csla;
using SurgeonPortal.DataAccess.Contracts.Trainees;
using SurgeonPortal.Library.Contracts.Trainees;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Exceptions;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Trainees.RegistrationStatusReadOnlyFactory;

namespace SurgeonPortal.Library.Trainees
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class RegistrationStatusReadOnly : YtgReadOnlyBase<RegistrationStatusReadOnly, int>, IRegistrationStatusReadOnly
    {
        private readonly IRegistrationStatusReadOnlyDal _registrationStatusReadOnlyDal;


        public RegistrationStatusReadOnly(
            IIdentityProvider identityProvider,
            IRegistrationStatusReadOnlyDal registrationStatusReadOnlyDal)
            : base(identityProvider)
        {
            _registrationStatusReadOnlyDal = registrationStatusReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(RegOpenDate))]
        public DateTime? RegOpenDate => ReadProperty(RegOpenDateProperty);
		public static readonly PropertyInfo<DateTime?> RegOpenDateProperty = RegisterProperty<DateTime?>(c => c.RegOpenDate);

        [DataMember]
		[DisplayName(nameof(RegEndDate))]
        public DateTime? RegEndDate => ReadProperty(RegEndDateProperty);
		public static readonly PropertyInfo<DateTime?> RegEndDateProperty = RegisterProperty<DateTime?>(c => c.RegEndDate);

        [DataMember]
		[DisplayName(nameof(isRegOpen))]
        public int isRegOpen => ReadProperty(isRegOpenProperty);
		public static readonly PropertyInfo<int> isRegOpenProperty = RegisterProperty<int>(c => c.isRegOpen);

        [DataMember]
		[DisplayName(nameof(RegLateDate))]
        public DateTime? RegLateDate => ReadProperty(RegLateDateProperty);
		public static readonly PropertyInfo<DateTime?> RegLateDateProperty = RegisterProperty<DateTime?>(c => c.RegLateDate);

        [DataMember]
		[DisplayName(nameof(isRegLate))]
        public int isRegLate => ReadProperty(isRegLateProperty);
		public static readonly PropertyInfo<int> isRegLateProperty = RegisterProperty<int>(c => c.isRegLate);


        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(RegistrationStatusReadOnly),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.TraineeClaim));
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByExamCode(GetByExamCodeCriteria criteria)
        
        {
            var dto = await _registrationStatusReadOnlyDal.GetByExamCodeAsync(criteria.ExamCode);
            
            if (dto == null)
            {
                throw new DataNotFoundException("RegistrationStatusReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(RegistrationStatusReadOnlyDto dto)
		{
            LoadProperty(RegOpenDateProperty, dto.RegOpenDate);
            LoadProperty(RegEndDateProperty, dto.RegEndDate);
            LoadProperty(isRegOpenProperty, dto.IsRegOpen);
            LoadProperty(RegLateDateProperty, dto.RegLateDate);
            LoadProperty(isRegLateProperty, dto.IsRegLate);
		} 
        
    }
}
