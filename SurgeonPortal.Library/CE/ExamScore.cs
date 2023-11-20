using Csla;
using Csla.Rules;
using SurgeonPortal.DataAccess.Contracts.CE;
using SurgeonPortal.Library.Contracts.CE;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.CE.ExamScoreFactory;

namespace SurgeonPortal.Library.CE
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class ExamScore : YtgBusinessBase<ExamScore>, IExamScore
    {
        private readonly IExamScoreDal _examScoreDal;
        private readonly IGetExamCasesScoredCommandFactory _getExamCasesScoredCommandFactory;

        public ExamScore(
            IIdentityProvider identityProvider,
            IExamScoreDal examScoreDal,
			IGetExamCasesScoredCommandFactory getExamCasesScoredCommandFactory)
            : base(identityProvider)
        {
            _examScoreDal = examScoreDal;
            _getExamCasesScoredCommandFactory = getExamCasesScoredCommandFactory;
        }

        [Key] 
        [DisplayName(nameof(ExamScheduleScoreId))]
		public int ExamScheduleScoreId
		{
			get { return GetProperty(ExamScheduleScoreIdProperty); }
			set { SetProperty(ExamScheduleScoreIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExamScheduleScoreIdProperty = RegisterProperty<int>(c => c.ExamScheduleScoreId);

        [DisplayName(nameof(ExamScheduleId))]
		public int ExamScheduleId
		{
			get { return GetProperty(ExamScheduleIdProperty); }
			set { SetProperty(ExamScheduleIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExamScheduleIdProperty = RegisterProperty<int>(c => c.ExamScheduleId);

        [DisplayName(nameof(ExaminerUserId))]
		public int ExaminerUserId
		{
			get { return GetProperty(ExaminerUserIdProperty); }
			 private set { SetProperty(ExaminerUserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExaminerUserIdProperty = RegisterProperty<int>(c => c.ExaminerUserId);

        [DisplayName(nameof(ExaminerScore))]
		public int ExaminerScore
		{
			get { return GetProperty(ExaminerScoreProperty); }
			set { SetProperty(ExaminerScoreProperty, value); }
		}
		public static readonly PropertyInfo<int> ExaminerScoreProperty = RegisterProperty<int>(c => c.ExaminerScore);

        [DisplayName(nameof(Submitted))]
		public bool? Submitted
		{
			get { return GetProperty(SubmittedProperty); }
			set { SetProperty(SubmittedProperty, value); }
		}
		public static readonly PropertyInfo<bool?> SubmittedProperty = RegisterProperty<bool?>(c => c.Submitted);

        [DisplayName(nameof(SubmittedDateTimeUTC))]
		public DateTime? SubmittedDateTimeUTC
		{
			get { return GetProperty(SubmittedDateTimeUTCProperty); }
			set { SetProperty(SubmittedDateTimeUTCProperty, value); }
		}
		public static readonly PropertyInfo<DateTime?> SubmittedDateTimeUTCProperty = RegisterProperty<DateTime?>(c => c.SubmittedDateTimeUTC);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(ExamScore),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));
            Csla.Rules.BusinessRules.AddRule(typeof(ExamScore),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));
            Csla.Rules.BusinessRules.AddRule(typeof(ExamScore),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));
        }

		protected override void AddInjectedBusinessRules()
		{
			BusinessRules.AddRule(new ExamCasesScoredRule(_getExamCasesScoredCommandFactory, 2));
		}

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetById(GetByIdCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _examScoreDal.GetByIdAsync(
                    criteria.ExamScheduleScoreId,
                    _identity.GetUserId<int>());
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("ExamScore not found based on criteria");
                }
                FetchData(dto);
            }
        }

        [Create]
        private void Create()
        {
            base.DataPortal_Create();
            LoadProperty(ExaminerUserIdProperty, _identity.GetUserId<int>());
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
                var dto = await _examScoreDal.InsertAsync(ToDto());
        
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
                var dto = await _examScoreDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(ExamScoreDto dto)
		{
            base.FetchData(dto);
            
			this.ExamScheduleScoreId = dto.ExamScheduleScoreId;
			this.ExamScheduleId = dto.ExamScheduleId;
			this.ExaminerUserId = dto.ExaminerUserId;
			this.ExaminerScore = dto.ExaminerScore;
			this.Submitted = dto.Submitted;
			this.SubmittedDateTimeUTC = dto.SubmittedDateTimeUTC;
		}

		internal ExamScoreDto ToDto()
		{
			var dto = new ExamScoreDto();
			return ToDto(dto);
		}
		internal ExamScoreDto ToDto(ExamScoreDto dto)
		{
            base.ToDto(dto);
            
			dto.ExamScheduleScoreId = this.ExamScheduleScoreId;
			dto.ExamScheduleId = this.ExamScheduleId;
			dto.ExaminerUserId = this.ExaminerUserId;
			dto.ExaminerScore = this.ExaminerScore;
			dto.Submitted = this.Submitted;
			dto.SubmittedDateTimeUTC = this.SubmittedDateTimeUTC;

			return dto;
		}
    }
}
