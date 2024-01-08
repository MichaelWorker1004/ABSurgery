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
using static SurgeonPortal.Library.Examinations.AdmissionCardAvailabilityReadOnlyFactory;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class AdmissionCardAvailabilityReadOnly : YtgReadOnlyBase<AdmissionCardAvailabilityReadOnly, int>, IAdmissionCardAvailabilityReadOnly
    {
        private readonly IAdmissionCardAvailabilityReadOnlyDal _admissionCardAvailabilityReadOnlyDal;


        public AdmissionCardAvailabilityReadOnly(
            IIdentityProvider identityProvider,
            IAdmissionCardAvailabilityReadOnlyDal admissionCardAvailabilityReadOnlyDal)
            : base(identityProvider)
        {
            _admissionCardAvailabilityReadOnlyDal = admissionCardAvailabilityReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(AdmCardAvailable))]
        public bool AdmCardAvailable => ReadProperty(AdmCardAvailableProperty);
		public static readonly PropertyInfo<bool> AdmCardAvailableProperty = RegisterProperty<bool>(c => c.AdmCardAvailable);

        [DataMember]
		[DisplayName(nameof(ExamId))]
        public int? ExamId => ReadProperty(ExamIdProperty);
		public static readonly PropertyInfo<int?> ExamIdProperty = RegisterProperty<int?>(c => c.ExamId);


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
            var dto = await _admissionCardAvailabilityReadOnlyDal.GetByExamIdAsync(
                _identity.GetUserId<int>(),
                criteria.ExamID);
            
            if (dto == null)
            {
                throw new DataNotFoundException("AdmissionCardAvailabilityReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(AdmissionCardAvailabilityReadOnlyDto dto)
		{
            LoadProperty(AdmCardAvailableProperty, dto.AdmCardAvailable);
            LoadProperty(ExamIdProperty, dto.ExamId);
		} 
        
    }
}
