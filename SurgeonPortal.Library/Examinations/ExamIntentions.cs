using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Examinations.ExamIntentionsFactory;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class ExamIntentions : YtgBusinessBase<ExamIntentions>, IExamIntentions
    {
        private readonly IExamIntentionsDal _examIntentionsDal;

        public ExamIntentions(
            IIdentityProvider identityProvider,
            IExamIntentionsDal examIntentionsDal)
            : base(identityProvider)
        {
            _examIntentionsDal = examIntentionsDal;
        }

        [DisplayName(nameof(UserId))]
        public int UserId
        {
            get { return GetProperty(UserIdProperty); }
            set { SetProperty(UserIdProperty, value); }
        }
        public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DisplayName(nameof(ExamId))]
        public int ExamId
        {
            get { return GetProperty(ExamIdProperty); }
            set { SetProperty(ExamIdProperty, value); }
        }
        public static readonly PropertyInfo<int> ExamIdProperty = RegisterProperty<int>(c => c.ExamId);

        [DisplayName(nameof(ExamName))]
		public string ExamName
		{
			get { return GetProperty(ExamNameProperty); }
			set { SetProperty(ExamNameProperty, value); }
		}
		public static readonly PropertyInfo<string> ExamNameProperty = RegisterProperty<string>(c => c.ExamName);

        [DisplayName(nameof(ExamDate))]
		public DateTime? ExamDate
		{
			get { return GetProperty(ExamDateProperty); }
			set { SetProperty(ExamDateProperty, value); }
		}
		public static readonly PropertyInfo<DateTime?> ExamDateProperty = RegisterProperty<DateTime?>(c => c.ExamDate);

        [DisplayName(nameof(DateReceived))]
		public DateTime? DateReceived
		{
			get { return GetProperty(DateReceivedProperty); }
			set { SetProperty(DateReceivedProperty, value); }
		}
		public static readonly PropertyInfo<DateTime?> DateReceivedProperty = RegisterProperty<DateTime?>(c => c.DateReceived);

        [DisplayName(nameof(Intention))]
		public bool Intention
		{
			get { return GetProperty(IntentionProperty); }
			set { SetProperty(IntentionProperty, value); }
		}
		public static readonly PropertyInfo<bool> IntentionProperty = RegisterProperty<bool>(c => c.Intention);



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
                var dto = await _examIntentionsDal.GetByExamIdAsync(
                    _identity.GetUserId<int>(),
                    criteria.ExamId);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("ExamIntentions not found based on criteria");
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
                var dto = await _examIntentionsDal.InsertAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(ExamIntentionsDto dto)
		{
            base.FetchData(dto);
            
			this.ExamName = dto.ExamName;
			this.ExamDate = dto.ExamDate;
			this.DateReceived = dto.DateReceived;
			this.Intention = dto.Intention;
		}

		internal ExamIntentionsDto ToDto()
		{
			var dto = new ExamIntentionsDto();
			return ToDto(dto);
		}
		internal ExamIntentionsDto ToDto(ExamIntentionsDto dto)
		{
            base.ToDto(dto);

            dto.UserId = this.UserId;
            dto.ExamId = this.ExamId;
			dto.ExamName = this.ExamName;
			dto.ExamDate = this.ExamDate;
			dto.DateReceived = this.DateReceived;
			dto.Intention = this.Intention;

			return dto;
		}


    }
}
