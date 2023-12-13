using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using SurgeonPortal.Library.Contracts.Email;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.ContinuousCertification.ReferenceLetterFactory;

namespace SurgeonPortal.Library.ContinuousCertification
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class ReferenceLetter : YtgBusinessBase<ReferenceLetter>, IReferenceLetter
    {
        private readonly IReferenceLetterDal _referenceLetterDal;
		private readonly IEmailFactory _emailFactory;

        public ReferenceLetter(
            IIdentityProvider identityProvider,
            IReferenceLetterDal referenceLetterDal,
			IEmailFactory emailFactory)
            : base(identityProvider)
        {
            _referenceLetterDal = referenceLetterDal;
			_emailFactory = emailFactory;
        }

        [DisplayName(nameof(UserId))]
		public int? UserId
		{
			get { return GetProperty(UserIdProperty); }
			set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> UserIdProperty = RegisterProperty<int?>(c => c.UserId);

		[Required]
        [DisplayName(nameof(Official))]
		public string Official
		{
			get { return GetProperty(OfficialProperty); }
			set { SetProperty(OfficialProperty, value); }
		}
		public static readonly PropertyInfo<string> OfficialProperty = RegisterProperty<string>(c => c.Official);

        [DisplayName(nameof(RoleName))]
		public string RoleName
		{
			get { return GetProperty(RoleNameProperty); }
			set { SetProperty(RoleNameProperty, value); }
		}
		public static readonly PropertyInfo<string> RoleNameProperty = RegisterProperty<string>(c => c.RoleName);

        [DisplayName(nameof(AltRoleName))]
		public string AltRoleName
		{
			get { return GetProperty(AltRoleNameProperty); }
			set { SetProperty(AltRoleNameProperty, value); }
		}
		public static readonly PropertyInfo<string> AltRoleNameProperty = RegisterProperty<string>(c => c.AltRoleName);

        [Key]
        [Required]
        [DisplayName(nameof(RoleId))]
		public int? RoleId
		{
			get { return GetProperty(RoleIdProperty); }
			set { SetProperty(RoleIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> RoleIdProperty = RegisterProperty<int?>(c => c.RoleId);

        [Key] 
        [DisplayName(nameof(AltRoleId))]
		public int? AltRoleId
		{
			get { return GetProperty(AltRoleIdProperty); }
			set { SetProperty(AltRoleIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> AltRoleIdProperty = RegisterProperty<int?>(c => c.AltRoleId);

        [DisplayName(nameof(Explain))]
		public string Explain
		{
			get { return GetProperty(ExplainProperty); }
			set { SetProperty(ExplainProperty, value); }
		}
		public static readonly PropertyInfo<string> ExplainProperty = RegisterProperty<string>(c => c.Explain);

        [Required]
        [DisplayName(nameof(Title))]
		public string Title
		{
			get { return GetProperty(TitleProperty); }
			set { SetProperty(TitleProperty, value); }
		}
		public static readonly PropertyInfo<string> TitleProperty = RegisterProperty<string>(c => c.Title);

        [Required]
        [DisplayName(nameof(Email))]
		public string Email
		{
			get { return GetProperty(EmailProperty); }
			set { SetProperty(EmailProperty, value); }
		}
		public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(c => c.Email);

        [Required]
        [DisplayName(nameof(Phone))]
		public string Phone
		{
			get { return GetProperty(PhoneProperty); }
			set { SetProperty(PhoneProperty, value); }
		}
		public static readonly PropertyInfo<string> PhoneProperty = RegisterProperty<string>(c => c.Phone);

        [Required]
        [DisplayName(nameof(Hosp))]
		public string Hosp
		{
			get { return GetProperty(HospProperty); }
			set { SetProperty(HospProperty, value); }
		}
		public static readonly PropertyInfo<string> HospProperty = RegisterProperty<string>(c => c.Hosp);

        [Required]
        [DisplayName(nameof(City))]
		public string City
		{
			get { return GetProperty(CityProperty); }
			set { SetProperty(CityProperty, value); }
		}
		public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(c => c.City);

        [Required]
        [DisplayName(nameof(State))]
		public string State
		{
			get { return GetProperty(StateProperty); }
			set { SetProperty(StateProperty, value); }
		}
		public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(c => c.State);

        [DisplayName(nameof(FullName))]
		public string FullName
		{
			get { return GetProperty(FullNameProperty); }
			set { SetProperty(FullNameProperty, value); }
		}
		public static readonly PropertyInfo<string> FullNameProperty = RegisterProperty<string>(c => c.FullName);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(ReferenceLetter),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
            Csla.Rules.BusinessRules.AddRule(typeof(ReferenceLetter),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.SurgeonClaim));
        }


        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetById(GetByIdCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _referenceLetterDal.GetByIdAsync(criteria.Id);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("ReferenceLetter not found based on criteria");
                }
                FetchData(dto);
            }
        }

        [Create]
        private void Create()
        {
            base.DataPortal_Create();
            LoadProperty(UserIdProperty, _identity.GetUserId<int>());
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
				var dto = await _referenceLetterDal.InsertAsync(ToDto());

				FetchData(dto);

				MarkIdle();
            }

			await SendReferenceLetter();
        }



        private void FetchData(ReferenceLetterDto dto)
		{
            base.FetchData(dto);
            
			this.UserId = dto.UserId;
			this.Official = dto.Official;
			this.RoleName = dto.RoleName;
			this.AltRoleName = dto.AltRoleName;
			this.RoleId = dto.RoleId;
			this.AltRoleId = dto.AltRoleId;
			this.Explain = dto.Explain;
			this.Title = dto.Title;
			this.Email = dto.Email;
			this.Phone = dto.Phone;
			this.Hosp = dto.Hosp;
			this.City = dto.City;
			this.State = dto.State;
			this.FullName = dto.FullName;
		}

		internal ReferenceLetterDto ToDto()
		{
			var dto = new ReferenceLetterDto();
			return ToDto(dto);
		}
		internal ReferenceLetterDto ToDto(ReferenceLetterDto dto)
		{
            base.ToDto(dto);
            
			dto.UserId = this.UserId;
			dto.Official = this.Official;
			dto.RoleName = this.RoleName;
			dto.AltRoleName = this.AltRoleName;
			dto.RoleId = this.RoleId;
			dto.AltRoleId = this.AltRoleId;
			dto.Explain = this.Explain;
			dto.Title = this.Title;
			dto.Email = this.Email;
			dto.Phone = this.Phone;
			dto.Hosp = this.Hosp;
			dto.City = this.City;
			dto.State = this.State;
			dto.FullName = this.FullName;

			return dto;
		}

		private async Task SendReferenceLetter()
		{
			var email = _emailFactory.Create();

			// TODO replace with template logic
			email.To = Email;
			email.Subject = "Mock Reference Letter";
			email.PlainTextContent = "This is a mock reference letter.";

			await email.SendAsync();
		}
    }
}
