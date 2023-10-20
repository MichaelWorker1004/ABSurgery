using Csla;
using SurgeonPortal.DataAccess.Contracts.Examiners;
using SurgeonPortal.Library.Contracts.Examiners;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;
using static SurgeonPortal.Library.Examiners.AgendaReadOnlyFactory;

namespace SurgeonPortal.Library.Examiners
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class AgendaReadOnly : ReadOnlyBase<AgendaReadOnly>, IAgendaReadOnly
    {
        private readonly IAgendaReadOnlyDal _agendaReadOnlyDal;


        public AgendaReadOnly(IAgendaReadOnlyDal agendaReadOnlyDal)
        {
            _agendaReadOnlyDal = agendaReadOnlyDal;
        }
        
        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DataMember]
		[DisplayName(nameof(DocumentName))]
        public string DocumentName => ReadProperty(DocumentNameProperty);
		public static readonly PropertyInfo<string> DocumentNameProperty = RegisterProperty<string>(c => c.DocumentName);


        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            Csla.Rules.BusinessRules.AddRule(typeof(AgendaReadOnly),
                new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, 
                    SurgeonPortal.Library.Contracts.Identity.SurgeonPortalClaims.ExaminerClaim));

        }
        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByExamHeaderId(GetByExamHeaderIdCriteria criteria)
        
        {
            var dto = await _agendaReadOnlyDal.GetByExamHeaderIdAsync(criteria.ExamHeaderId);
            
            if (dto == null)
            {
                throw new DataNotFoundException("AgendaReadOnly not found based on criteria.");
            }
            
            FetchData(dto);
        }
        
		private void FetchData(AgendaReadOnlyDto dto)
		{
            LoadProperty(IdProperty, dto.Id);
            LoadProperty(DocumentNameProperty, dto.DocumentName);
		} 
        
    }
}
