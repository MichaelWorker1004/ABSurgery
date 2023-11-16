using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring.CE;
using SurgeonPortal.Library.Contracts.Scoring.CE;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Exceptions;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Scoring.CE.ExamineeReadOnlyFactory;

namespace SurgeonPortal.Library.Scoring.CE
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class ExamineeReadOnly : YtgReadOnlyBase<ExamineeReadOnly, int>, IExamineeReadOnly
    {
        private readonly IExamineeReadOnlyDal _examineeReadOnlyDal;


        public ExamineeReadOnly(
            IIdentityProvider identityProvider,
            IExamineeReadOnlyDal examineeReadOnlyDal)
            : base(identityProvider)
        {
            _examineeReadOnlyDal = examineeReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(ExamScheduleId))]
        public int ExamScheduleId => ReadProperty(ExamScheduleIdProperty);
		public static readonly PropertyInfo<int> ExamScheduleIdProperty = RegisterProperty<int>(c => c.ExamScheduleId);

        [DataMember]
		[DisplayName(nameof(FullName))]
        public string FullName => ReadProperty(FullNameProperty);
		public static readonly PropertyInfo<string> FullNameProperty = RegisterProperty<string>(c => c.FullName);

        [DataMember]
		[DisplayName(nameof(ExamDate))]
        public string ExamDate => ReadProperty(ExamDateProperty);
		public static readonly PropertyInfo<string> ExamDateProperty = RegisterProperty<string>(c => c.ExamDate);

		public static readonly PropertyInfo<TitleReadOnlyList> CasesProperty =
            RegisterProperty<TitleReadOnlyList>(c => c.Cases);
        public ITitleReadOnlyList Cases
        {
            get => GetProperty(CasesProperty);
            private set => LoadProperty(CasesProperty, value);
        }

        [DataMember]
		[DisplayName(nameof(ExamineeUserId))]
        public int ExamineeUserId => ReadProperty(ExamineeUserIdProperty);
		public static readonly PropertyInfo<int> ExamineeUserIdProperty = RegisterProperty<int>(c => c.ExamineeUserId);

        [DataMember]
		[DisplayName(nameof(ExamScoringId))]
        public int ExamScoringId => ReadProperty(ExamScoringIdProperty);
		public static readonly PropertyInfo<int> ExamScoringIdProperty = RegisterProperty<int>(c => c.ExamScoringId);

        [DataMember]
		[DisplayName(nameof(TimerBit))]
        public bool? TimerBit => ReadProperty(TimerBitProperty);
		public static readonly PropertyInfo<bool?> TimerBitProperty = RegisterProperty<bool?>(c => c.TimerBit);


        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(ExamineeReadOnly),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetById(GetByIdCriteria criteria)
        
        {
            var dto = await _examineeReadOnlyDal.GetByIdAsync(criteria.ExamScheduleId);
            
            if (dto == null)
            {
                throw new DataNotFoundException("ExamineeReadOnly not found based on criteria.");
            }
            
			await FetchDataAsync(dto);
        }
        
		private async Task FetchDataAsync(ExamineeReadOnlyDto dto)
		{
            LoadProperty(ExamScheduleIdProperty, dto.ExamScheduleId);
            LoadProperty(FullNameProperty, dto.FullName);
            LoadProperty(ExamDateProperty, dto.ExamDate);
			LoadProperty(CasesProperty, await DataPortal.FetchAsync<TitleReadOnlyList>(new TitleReadOnlyListFactory.GetByIdCriteria(dto.ExamScheduleId, dto.ExamineeUserId)));
            LoadProperty(ExamineeUserIdProperty, dto.ExamineeUserId);
            LoadProperty(ExamScoringIdProperty, dto.ExamScoringId);
            LoadProperty(TimerBitProperty, dto.TimerBit);
		} 
        
    }
}
