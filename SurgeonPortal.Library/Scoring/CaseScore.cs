using Csla;
using Csla.Core;
using Csla.Rules;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Scoring.CaseScoreFactory;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class CaseScore : YtgBusinessBase<CaseScore>, ICaseScore
    {
        private readonly ICaseScoreDal _caseScoreDal;

        public CaseScore(
            IIdentityProvider identityProvider,
            ICaseScoreDal caseScoreDal)
            : base(identityProvider)
        {
            _caseScoreDal = caseScoreDal;
        }

        [Key] 
        [DisplayName(nameof(ExamScoringId))]
		public int ExamScoringId
		{
			get { return GetProperty(ExamScoringIdProperty); }
			set { SetProperty(ExamScoringIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExamScoringIdProperty = RegisterProperty<int>(c => c.ExamScoringId);

        [DisplayName(nameof(ExamCaseId))]
		public int ExamCaseId
		{
			get { return GetProperty(ExamCaseIdProperty); }
			set { SetProperty(ExamCaseIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExamCaseIdProperty = RegisterProperty<int>(c => c.ExamCaseId);

        [DisplayName(nameof(ExaminerUserId))]
		public int ExaminerUserId
		{
			get { return GetProperty(ExaminerUserIdProperty); }
			set { SetProperty(ExaminerUserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExaminerUserIdProperty = RegisterProperty<int>(c => c.ExaminerUserId);

        [DisplayName(nameof(ExamineeUserId))]
		public int ExamineeUserId
		{
			get { return GetProperty(ExamineeUserIdProperty); }
			set { SetProperty(ExamineeUserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> ExamineeUserIdProperty = RegisterProperty<int>(c => c.ExamineeUserId);

        [DisplayName(nameof(ExamineeFirstName))]
		public string ExamineeFirstName
		{
			get { return GetProperty(ExamineeFirstNameProperty); }
			set { SetProperty(ExamineeFirstNameProperty, value); }
		}
		public static readonly PropertyInfo<string> ExamineeFirstNameProperty = RegisterProperty<string>(c => c.ExamineeFirstName);

        [DisplayName(nameof(ExamineeMiddleName))]
		public string ExamineeMiddleName
		{
			get { return GetProperty(ExamineeMiddleNameProperty); }
			set { SetProperty(ExamineeMiddleNameProperty, value); }
		}
		public static readonly PropertyInfo<string> ExamineeMiddleNameProperty = RegisterProperty<string>(c => c.ExamineeMiddleName);

        [DisplayName(nameof(ExamineeLastName))]
		public string ExamineeLastName
		{
			get { return GetProperty(ExamineeLastNameProperty); }
			set { SetProperty(ExamineeLastNameProperty, value); }
		}
		public static readonly PropertyInfo<string> ExamineeLastNameProperty = RegisterProperty<string>(c => c.ExamineeLastName);

        [DisplayName(nameof(ExamineeSuffix))]
		public string ExamineeSuffix
		{
			get { return GetProperty(ExamineeSuffixProperty); }
			set { SetProperty(ExamineeSuffixProperty, value); }
		}
		public static readonly PropertyInfo<string> ExamineeSuffixProperty = RegisterProperty<string>(c => c.ExamineeSuffix);

        [DisplayName(nameof(Score))]
		public int Score
		{
			get { return GetProperty(ScoreProperty); }
			set { SetProperty(ScoreProperty, value); }
		}
		public static readonly PropertyInfo<int> ScoreProperty = RegisterProperty<int>(c => c.Score);

        [DisplayName(nameof(CriticalFail))]
		public bool? CriticalFail
		{
			get { return GetProperty(CriticalFailProperty); }
			set { SetProperty(CriticalFailProperty, value); }
		}
		public static readonly PropertyInfo<bool?> CriticalFailProperty = RegisterProperty<bool?>(c => c.CriticalFail);

        [DisplayName(nameof(Remarks))]
		public string Remarks
		{
			get { return GetProperty(RemarksProperty); }
			set { SetProperty(RemarksProperty, value); }
		}
		public static readonly PropertyInfo<string> RemarksProperty = RegisterProperty<string>(c => c.Remarks);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(CaseScore),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));

            Csla.Rules.BusinessRules.AddRule(typeof(CaseScore),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));

            Csla.Rules.BusinessRules.AddRule(typeof(CaseScore),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));

            Csla.Rules.BusinessRules.AddRule(typeof(CaseScore),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));

        }

		protected override void AddBusinessRules()
		{
			BusinessRules.AddRule(new ExamLockedRule(ExamCaseIdProperty, 1));
		}



        [RunLocal]
        [DeleteSelf]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task DeleteSelf()
        {
            using (BypassPropertyChecks)
            {
                base.DataPortal_DeleteSelf();
        
                await _caseScoreDal.DeleteAsync(ToDto());
        
                MarkIdle();
            }
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetById(GetByIdCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _caseScoreDal.GetByIdAsync(criteria.ExamScoringId);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("CaseScore not found based on criteria");
                }
                FetchData(dto);
            }
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
                var dto = await _caseScoreDal.InsertAsync(ToDto());
        
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
                var dto = await _caseScoreDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(CaseScoreDto dto)
		{
            base.FetchData(dto);
            
			this.ExamScoringId = dto.ExamScoringId;
			this.ExamCaseId = dto.ExamCaseId;
			this.ExaminerUserId = dto.ExaminerUserId;
			this.ExamineeUserId = dto.ExamineeUserId;
			this.ExamineeFirstName = dto.ExamineeFirstName;
			this.ExamineeMiddleName = dto.ExamineeMiddleName;
			this.ExamineeLastName = dto.ExamineeLastName;
			this.ExamineeSuffix = dto.ExamineeSuffix;
			this.Score = dto.Score;
			this.CriticalFail = dto.CriticalFail;
			this.Remarks = dto.Remarks;
		}

		internal CaseScoreDto ToDto()
		{
			var dto = new CaseScoreDto();
			return ToDto(dto);
		}
		internal CaseScoreDto ToDto(CaseScoreDto dto)
		{
            base.ToDto(dto);
            
			dto.ExamScoringId = this.ExamScoringId;
			dto.ExamCaseId = this.ExamCaseId;
			dto.ExaminerUserId = this.ExaminerUserId;
			dto.ExamineeUserId = this.ExamineeUserId;
			dto.ExamineeFirstName = this.ExamineeFirstName;
			dto.ExamineeMiddleName = this.ExamineeMiddleName;
			dto.ExamineeLastName = this.ExamineeLastName;
			dto.ExamineeSuffix = this.ExamineeSuffix;
			dto.Score = this.Score;
			dto.CriticalFail = this.CriticalFail;
			dto.Remarks = this.Remarks;

			return dto;
		}


    }

	public class ExamLockedRule : BusinessRule
	{
		private IIsExamSessionLockedCommandFactory _isExamSessionLockedCommandFactory;

		public ExamLockedRule(IPropertyInfo primaryProperty,
			int priority)
			: base(primaryProperty)
		{
			_isExamSessionLockedCommandFactory = new IsExamSessionLockedCommandFactory();
			InputProperties = new List<IPropertyInfo> { primaryProperty };
			Priority = priority;
		}

		protected override void Execute(IRuleContext context)
		{
			var examCaseId = (int)context.InputPropertyValues[PrimaryProperty];

			var command = _isExamSessionLockedCommandFactory.IsExamSessionLocked(examCaseId);

			if(command.IsLocked.HasValue && command.IsLocked.Value)
			{
				context.AddErrorResult("Cannot add or update score when exam is locked.");
			}
		}
	}
}
