using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Exceptions;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Examinations.ExamTitleReadOnlyFactory;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class ExamTitleReadOnly : YtgReadOnlyBase<ExamTitleReadOnly, int>, IExamTitleReadOnly
    {
        private readonly IExamTitleReadOnlyDal _examTitleReadOnlyDal;


        public ExamTitleReadOnly(
            IIdentityProvider identityProvider,
            IExamTitleReadOnlyDal examTitleReadOnlyDal)
            : base(identityProvider)
        {
            _examTitleReadOnlyDal = examTitleReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(ExamName))]
        public string ExamName => ReadProperty(ExamNameProperty);
		public static readonly PropertyInfo<string> ExamNameProperty = RegisterProperty<string>(c => c.ExamName);


        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(ExamTitleReadOnly),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));
        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByExamId(GetByExamIdCriteria criteria)
        
        {
            var dto = await _examTitleReadOnlyDal.GetByExamIdAsync(criteria.ExamId);
            
            if (dto == null)
            {
                throw new DataNotFoundException("ExamTitleReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(ExamTitleReadOnlyDto dto)
		{
            LoadProperty(ExamNameProperty, dto.ExamName);
		} 
        
    }
}
