using Csla;
using SurgeonPortal.DataAccess.Contracts.Examiners;
using SurgeonPortal.Library.Contracts.Examiners;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;
using static SurgeonPortal.Library.Examiners.ConflictReadOnlyFactory;

namespace SurgeonPortal.Library.Examiners
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class ConflictReadOnly : ReadOnlyBase<ConflictReadOnly>, IConflictReadOnly
    {
        private readonly IConflictReadOnlyDal _conflictReadOnlyDal;


        public ConflictReadOnly(IConflictReadOnlyDal conflictReadOnlyDal)
        {
            _conflictReadOnlyDal = conflictReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(StreamId))]
        public Guid StreamId => ReadProperty(StreamIdProperty);
		public static readonly PropertyInfo<Guid> StreamIdProperty = RegisterProperty<Guid>(c => c.StreamId);

        [DataMember]
		[DisplayName(nameof(name))]
        public string name => ReadProperty(nameProperty);
		public static readonly PropertyInfo<string> nameProperty = RegisterProperty<string>(c => c.name);


        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(ConflictReadOnly),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));

        }
        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByExamHeaderId(GetByExamHeaderIdCriteria criteria)
        
        {
            var dto = await _conflictReadOnlyDal.GetByExamHeaderIdAsync(criteria.ExamHeaderId);
            
            if (dto == null)
            {
                throw new DataNotFoundException("ConflictReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(ConflictReadOnlyDto dto)
		{
            LoadProperty(StreamIdProperty, dto.StreamId);
            LoadProperty(nameProperty, dto.Name);
		} 
        
    }
}
