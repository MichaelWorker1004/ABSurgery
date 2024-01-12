using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Exceptions;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.ContinuousCertification.RequirementsReadOnlyFactory;

namespace SurgeonPortal.Library.ContinuousCertification
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class RequirementsReadOnly : YtgReadOnlyBase<RequirementsReadOnly, int>, IRequirementsReadOnly
    {
        private readonly IRequirementsReadOnlyDal _requirementsReadOnlyDal;


        public RequirementsReadOnly(
            IIdentityProvider identityProvider,
            IRequirementsReadOnlyDal requirementsReadOnlyDal)
            : base(identityProvider)
        {
            _requirementsReadOnlyDal = requirementsReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(MeetingRequirements))]
        public string MeetingRequirements => ReadProperty(MeetingRequirementsProperty);
		public static readonly PropertyInfo<string> MeetingRequirementsProperty = RegisterProperty<string>(c => c.MeetingRequirements);


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
        private async Task GetByUserId(GetByUserIdCriteria criteria)
        
        {
            var dto = await _requirementsReadOnlyDal.GetByUserIdAsync(criteria.UserId);
            
            if (dto == null)
            {
                throw new DataNotFoundException("RequirementsReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(RequirementsReadOnlyDto dto)
		{
            LoadProperty(MeetingRequirementsProperty, dto.MeetingRequirements);
		} 
        
    }
}
