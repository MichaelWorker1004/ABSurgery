using Csla;
using Microsoft.Extensions.Options;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Email;
using SurgeonPortal.Library.Contracts.Examinations;
using SurgeonPortal.Shared.ReferenceLetters;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Examinations.PdReferenceLetterFactory;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class PdReferenceLetter : YtgBusinessBase<PdReferenceLetter>, IPdReferenceLetter
    {
        private readonly IPdReferenceLetterDal _pdReferenceLetterDal;
        private readonly IEmailFactory _emailFactory;
        private readonly ReferenceLettersConfiguration _referenceLettersConfiguration;

        public PdReferenceLetter(
            IIdentityProvider identityProvider,
            IPdReferenceLetterDal pdReferenceLetterDal,
            IEmailFactory emailFactory,
            IOptions<ReferenceLettersConfiguration> referenceLettersConfiguration)
            : base(identityProvider)
        {
            _pdReferenceLetterDal = pdReferenceLetterDal;
            _emailFactory = emailFactory;
            _referenceLettersConfiguration = referenceLettersConfiguration.Value;
        }

        [Key] 
        [DisplayName(nameof(Id))]
		public decimal Id
		{
			get { return GetProperty(IdProperty); }
			set { SetProperty(IdProperty, value); }
		}
		public static readonly PropertyInfo<decimal> IdProperty = RegisterProperty<decimal>(c => c.Id);

        [DisplayName(nameof(UserId))]
		public int? UserId
		{
			get { return GetProperty(UserIdProperty); }
			set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int?> UserIdProperty = RegisterProperty<int?>(c => c.UserId);

        [DisplayName(nameof(Hosp))]
		public string Hosp
		{
			get { return GetProperty(HospProperty); }
			set { SetProperty(HospProperty, value); }
		}
		public static readonly PropertyInfo<string> HospProperty = RegisterProperty<string>(c => c.Hosp);

        [DisplayName(nameof(Official))]
		public string Official
		{
			get { return GetProperty(OfficialProperty); }
			set { SetProperty(OfficialProperty, value); }
		}
		public static readonly PropertyInfo<string> OfficialProperty = RegisterProperty<string>(c => c.Official);

        [DisplayName(nameof(Title))]
		public string Title
		{
			get { return GetProperty(TitleProperty); }
			set { SetProperty(TitleProperty, value); }
		}
		public static readonly PropertyInfo<string> TitleProperty = RegisterProperty<string>(c => c.Title);

        [DisplayName(nameof(Email))]
		public string Email
		{
			get { return GetProperty(EmailProperty); }
			set { SetProperty(EmailProperty, value); }
		}
		public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(c => c.Email);

        [DisplayName(nameof(LetterSent))]
		public DateTime? LetterSent
		{
			get { return GetProperty(LetterSentProperty); }
			set { SetProperty(LetterSentProperty, value); }
		}
		public static readonly PropertyInfo<DateTime?> LetterSentProperty = RegisterProperty<DateTime?>(c => c.LetterSent);

        [DisplayName(nameof(Status))]
		public int Status
		{
			get { return GetProperty(StatusProperty); }
			set { SetProperty(StatusProperty, value); }
		}
		public static readonly PropertyInfo<int> StatusProperty = RegisterProperty<int>(c => c.Status);

        [DisplayName(nameof(ExamId))]
        public int ExamId
        {
            get { return GetProperty(ExamIdProperty); }
            set { SetProperty(ExamIdProperty, value); }
        }
        public static readonly PropertyInfo<int> ExamIdProperty = RegisterProperty<int>(c => c.ExamId);

        [DisplayName(nameof(IdCode))]
        public string IdCode
        {
            get { return GetProperty(IdCodeProperty); }
            set { SetProperty(IdCodeProperty, value); }
        }
        public static readonly PropertyInfo<string> IdCodeProperty = RegisterProperty<string>(c => c.IdCode);

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
            
            
        }


        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByExamId(GetByExamIdCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _pdReferenceLetterDal.GetByExamIdAsync(
                    _identity.GetUserId<int>(),
                    criteria.ExamId);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("PdReferenceLetter not found based on criteria");
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
                var dto = await _pdReferenceLetterDal.InsertAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(PdReferenceLetterDto dto)
		{
            base.FetchData(dto);
            
			this.Id = dto.Id;
			this.UserId = dto.UserId;
			this.Hosp = dto.Hosp;
			this.Official = dto.Official;
			this.Title = dto.Title;
			this.Email = dto.Email;
			this.LetterSent = dto.LetterSent;
			this.Status = dto.Status;
		}

		internal PdReferenceLetterDto ToDto()
		{
			var dto = new PdReferenceLetterDto();
			return ToDto(dto);
		}
		internal PdReferenceLetterDto ToDto(PdReferenceLetterDto dto)
		{
            base.ToDto(dto);
            
			dto.Id = this.Id;
			dto.UserId = this.UserId;
			dto.Hosp = this.Hosp;
			dto.Official = this.Official;
			dto.Title = this.Title;
			dto.Email = this.Email;
			dto.LetterSent = this.LetterSent;
			dto.Status = this.Status;
            dto.ExamId = this.ExamId;
            dto.IdCode = this.IdCode;

			return dto;
		}

        private string GenerateIdCode()
        {
            const string SecOrder = "10";

            var sha = SHA1.Create();
            return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes($"{Email}{UserId}{SecOrder}")));
        }

        private async Task SendReferenceLetter()
        {
            var email = _emailFactory.Create();

            var url = $"{_referenceLettersConfiguration.Url}?pd=0&reflet={IdCode}";

            email.To = Email;
            email.TemplateId = _referenceLettersConfiguration.EmailTemplateId;
            var recipientNameSplit = Official.Split(' ', count: 2);
            var candidateNameSplit = FullName.Split(' ', count: 2);
            email.TemplateData = new
            {
                recipient_first_name = recipientNameSplit[0],
                recipient_last_name = recipientNameSplit.ElementAtOrDefault(1),
                candidate_first_name = candidateNameSplit[0],
                candidate_last_name = candidateNameSplit.ElementAtOrDefault(1),
                url
            };

            await email.SendAsync();
        }
    }
}
