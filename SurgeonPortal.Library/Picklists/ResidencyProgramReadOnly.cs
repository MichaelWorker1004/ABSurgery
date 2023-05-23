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
    public class ResidencyProgramReadOnly : ReadOnlyBase<ResidencyProgramReadOnly>, IResidencyProgramReadOnly
    {
        [DataMember]
		[DisplayName(nameof(ProgramId))]
        public int ProgramId => ReadProperty(ProgramIdProperty);
		public static readonly PropertyInfo<int> ProgramIdProperty = RegisterProperty<int>(c => c.ProgramId);

        [DataMember]
		[DisplayName(nameof(ProgramName))]
        public string ProgramName => ReadProperty(ProgramNameProperty);
		public static readonly PropertyInfo<string> ProgramNameProperty = RegisterProperty<string>(c => c.ProgramName);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(ResidencyProgramReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(ResidencyProgramReadOnlyDto dto)
		{
            LoadProperty(ProgramIdProperty, dto.ProgramId);
            LoadProperty(ProgramNameProperty, dto.ProgramName);
		} 
        
    }
}
