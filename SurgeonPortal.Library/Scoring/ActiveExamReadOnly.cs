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
using static SurgeonPortal.Library.Scoring.ActiveExamReadOnlyFactory;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class ActiveExamReadOnly : YtgReadOnlyBase<ActiveExamReadOnly, int>, IActiveExamReadOnly
    {
        private readonly IActiveExamReadOnlyDal _activeExamReadOnlyDal;


        public ActiveExamReadOnly(
            IIdentityProvider identityProvider,
            IActiveExamReadOnlyDal activeExamReadOnlyDal)
            : base(identityProvider)
        {
            _activeExamReadOnlyDal = activeExamReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(ExamHeaderId))]
        public int ExamHeaderId => ReadProperty(ExamHeaderIdProperty);
		public static readonly PropertyInfo<int> ExamHeaderIdProperty = RegisterProperty<int>(c => c.ExamHeaderId);


        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(ActiveExamReadOnly),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByExaminerUserId(GetByExaminerUserIdCriteria criteria)
        
        {
            var dto = await _activeExamReadOnlyDal.GetByExaminerUserIdAsync(
                _identity.GetUserId<int>(),
                criteria.CurrentDate);
            
            if (dto == null)
            {
                throw new DataNotFoundException("ActiveExamReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(ActiveExamReadOnlyDto dto)
		{
            LoadProperty(ExamHeaderIdProperty, dto.ExamHeaderId);
		} 
        
    }
}
