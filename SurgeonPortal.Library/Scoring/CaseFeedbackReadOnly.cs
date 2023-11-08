using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Exceptions;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Scoring.CaseFeedbackReadOnlyFactory;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class CaseFeedbackReadOnly : YtgReadOnlyBase<CaseFeedbackReadOnly, int>, ICaseFeedbackReadOnly
    {
        private readonly ICaseFeedbackReadOnlyDal _caseFeedbackReadOnlyDal;


        public CaseFeedbackReadOnly(
            IIdentityProvider identityProvider,
            ICaseFeedbackReadOnlyDal caseFeedbackReadOnlyDal)
            : base(identityProvider)
        {
            _caseFeedbackReadOnlyDal = caseFeedbackReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DataMember]
		[DisplayName(nameof(UserId))]
        public int UserId => ReadProperty(UserIdProperty);
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DataMember]
		[DisplayName(nameof(CaseHeaderId))]
        public int CaseHeaderId => ReadProperty(CaseHeaderIdProperty);
		public static readonly PropertyInfo<int> CaseHeaderIdProperty = RegisterProperty<int>(c => c.CaseHeaderId);

        [DataMember]
		[DisplayName(nameof(Feedback))]
        public string Feedback => ReadProperty(FeedbackProperty);
		public static readonly PropertyInfo<string> FeedbackProperty = RegisterProperty<string>(c => c.Feedback);


        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(CaseFeedbackReadOnly),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByExaminerId(GetByExaminerIdCriteria criteria)
        
        {
            var dto = await _caseFeedbackReadOnlyDal.GetByExaminerIdAsync(
                _identity.GetUserId<int>(),
                criteria.CaseHeaderId);
            
            if (dto == null)
            {
                throw new DataNotFoundException("CaseFeedbackReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(CaseFeedbackReadOnlyDto dto)
		{
            LoadProperty(IdProperty, dto.Id);
            LoadProperty(UserIdProperty, dto.UserId);
            LoadProperty(CaseHeaderIdProperty, dto.CaseHeaderId);
            LoadProperty(FeedbackProperty, dto.Feedback);
		} 
        
    }
}
