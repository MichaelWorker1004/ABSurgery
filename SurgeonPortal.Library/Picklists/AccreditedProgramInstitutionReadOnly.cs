using Csla;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SurgeonPortal.Library.Picklists
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class AccreditedProgramInstitutionReadOnly : ReadOnlyBase<AccreditedProgramInstitutionReadOnly>, IAccreditedProgramInstitutionReadOnly
    {
        [DataMember]
		[DisplayName(nameof(ProgramId))]
        public int ProgramId => ReadProperty(ProgramIdProperty);
		public static readonly PropertyInfo<int> ProgramIdProperty = RegisterProperty<int>(c => c.ProgramId);

        [DataMember]
		[DisplayName(nameof(InstitutionName))]
        public string InstitutionName => ReadProperty(InstitutionNameProperty);
		public static readonly PropertyInfo<string> InstitutionNameProperty = RegisterProperty<string>(c => c.InstitutionName);

        [DataMember]
		[DisplayName(nameof(City))]
        public string City => ReadProperty(CityProperty);
		public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(c => c.City);

        [DataMember]
		[DisplayName(nameof(State))]
        public string State => ReadProperty(StateProperty);
		public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(c => c.State);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(AccreditedProgramInstitutionReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(AccreditedProgramInstitutionReadOnlyDto dto)
		{
            LoadProperty(ProgramIdProperty, dto.ProgramId);
            LoadProperty(InstitutionNameProperty, dto.InstitutionName);
            LoadProperty(CityProperty, dto.City);
            LoadProperty(StateProperty, dto.State);
		} 
        
    }
}
