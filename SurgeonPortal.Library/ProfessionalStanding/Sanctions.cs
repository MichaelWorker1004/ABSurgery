using Csla;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.ProfessionalStanding.SanctionsFactory;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class Sanctions : YtgBusinessBase<Sanctions>, ISanctions
    {
        private readonly ISanctionsDal _sanctionsDal;

        public Sanctions(
            IIdentityProvider identityProvider,
            ISanctionsDal sanctionsDal)
            : base(identityProvider)
        {
            _sanctionsDal = sanctionsDal;
        }

        [Key] 
        [DisplayName(nameof(Id))]
		public int Id
		{
			get { return GetProperty(IdProperty); }
			set { SetProperty(IdProperty, value); }
		}
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DisplayName(nameof(UserId))]
		public int UserId
		{
			get { return GetProperty(UserIdProperty); }
			set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DisplayName(nameof(HadDrugAlchoholTreatment))]
		public bool HadDrugAlchoholTreatment
		{
			get { return GetProperty(HadDrugAlchoholTreatmentProperty); }
			set { SetProperty(HadDrugAlchoholTreatmentProperty, value); }
		}
		public static readonly PropertyInfo<bool> HadDrugAlchoholTreatmentProperty = RegisterProperty<bool>(c => c.HadDrugAlchoholTreatment);

        [DisplayName(nameof(HadHospitalPrivilegesDenied))]
		public bool HadHospitalPrivilegesDenied
		{
			get { return GetProperty(HadHospitalPrivilegesDeniedProperty); }
			set { SetProperty(HadHospitalPrivilegesDeniedProperty, value); }
		}
		public static readonly PropertyInfo<bool> HadHospitalPrivilegesDeniedProperty = RegisterProperty<bool>(c => c.HadHospitalPrivilegesDenied);

        [DisplayName(nameof(HadLicenseRestricted))]
		public bool HadLicenseRestricted
		{
			get { return GetProperty(HadLicenseRestrictedProperty); }
			set { SetProperty(HadLicenseRestrictedProperty, value); }
		}
		public static readonly PropertyInfo<bool> HadLicenseRestrictedProperty = RegisterProperty<bool>(c => c.HadLicenseRestricted);

        [DisplayName(nameof(HadHospitalPrivilegesRestricted))]
		public bool HadHospitalPrivilegesRestricted
		{
			get { return GetProperty(HadHospitalPrivilegesRestrictedProperty); }
			set { SetProperty(HadHospitalPrivilegesRestrictedProperty, value); }
		}
		public static readonly PropertyInfo<bool> HadHospitalPrivilegesRestrictedProperty = RegisterProperty<bool>(c => c.HadHospitalPrivilegesRestricted);

        [DisplayName(nameof(HadFelonyConviction))]
		public bool HadFelonyConviction
		{
			get { return GetProperty(HadFelonyConvictionProperty); }
			set { SetProperty(HadFelonyConvictionProperty, value); }
		}
		public static readonly PropertyInfo<bool> HadFelonyConvictionProperty = RegisterProperty<bool>(c => c.HadFelonyConviction);

        [DisplayName(nameof(HadCensure))]
		public bool HadCensure
		{
			get { return GetProperty(HadCensureProperty); }
			set { SetProperty(HadCensureProperty, value); }
		}
		public static readonly PropertyInfo<bool> HadCensureProperty = RegisterProperty<bool>(c => c.HadCensure);

        [DisplayName(nameof(Explanation))]
		public string Explanation
		{
			get { return GetProperty(ExplanationProperty); }
			set { SetProperty(ExplanationProperty, value); }
		}
		public static readonly PropertyInfo<string> ExplanationProperty = RegisterProperty<string>(c => c.Explanation);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(Sanctions),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

            Csla.Rules.BusinessRules.AddRule(typeof(Sanctions),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

            Csla.Rules.BusinessRules.AddRule(typeof(Sanctions),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));

        }




        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId()
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _sanctionsDal.GetByUserIdAsync(_identity.GetUserId<int>());
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("Sanctions not found based on criteria");
                }
                FetchData(dto);
            }
        }

        [Create]
        private void Create()
        {
            LoadProperty(UserIdProperty, _identity.GetUserId<int>());
            LoadProperty(CreatedByUserIdProperty, _identity.GetUserId<int>());
            LoadProperty(LastUpdatedByUserIdProperty, _identity.GetUserId<int>());
        }
        
        [RunLocal]
        [Insert]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task Insert()
        {
            base.DataPortal_Insert();
        
            using (BypassPropertyChecks)
            {
                var dto = await _sanctionsDal.InsertAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }

        [RunLocal]
        [Update]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task Update()
        {
            base.DataPortal_Update();
        
            using (BypassPropertyChecks)
            {
                var dto = await _sanctionsDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(SanctionsDto dto)
		{
            base.FetchData(dto);
            
			this.Id = dto.Id;
			this.UserId = dto.UserId;
			this.HadDrugAlchoholTreatment = dto.HadDrugAlchoholTreatment;
			this.HadHospitalPrivilegesDenied = dto.HadHospitalPrivilegesDenied;
			this.HadLicenseRestricted = dto.HadLicenseRestricted;
			this.HadHospitalPrivilegesRestricted = dto.HadHospitalPrivilegesRestricted;
			this.HadFelonyConviction = dto.HadFelonyConviction;
			this.HadCensure = dto.HadCensure;
			this.Explanation = dto.Explanation;
		}

		internal SanctionsDto ToDto()
		{
			var dto = new SanctionsDto();
			return ToDto(dto);
		}
		internal SanctionsDto ToDto(SanctionsDto dto)
		{
            base.ToDto(dto);
            
			dto.Id = this.Id;
			dto.UserId = this.UserId;
			dto.HadDrugAlchoholTreatment = this.HadDrugAlchoholTreatment;
			dto.HadHospitalPrivilegesDenied = this.HadHospitalPrivilegesDenied;
			dto.HadLicenseRestricted = this.HadLicenseRestricted;
			dto.HadHospitalPrivilegesRestricted = this.HadHospitalPrivilegesRestricted;
			dto.HadFelonyConviction = this.HadFelonyConviction;
			dto.HadCensure = this.HadCensure;
			dto.Explanation = this.Explanation;

			return dto;
		}


    }
}
