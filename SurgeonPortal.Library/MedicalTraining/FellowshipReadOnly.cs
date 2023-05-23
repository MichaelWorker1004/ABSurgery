using Csla;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SurgeonPortal.Library.MedicalTraining
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class FellowshipReadOnly : ReadOnlyBase<FellowshipReadOnly>, IFellowshipReadOnly
    {
        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DataMember]
		[DisplayName(nameof(ProgramName))]
        public string ProgramName => ReadProperty(ProgramNameProperty);
		public static readonly PropertyInfo<string> ProgramNameProperty = RegisterProperty<string>(c => c.ProgramName);

        [DataMember]
		[DisplayName(nameof(CompletionYear))]
        public string CompletionYear => ReadProperty(CompletionYearProperty);
		public static readonly PropertyInfo<string> CompletionYearProperty = RegisterProperty<string>(c => c.CompletionYear);

        [DataMember]
		[DisplayName(nameof(ProgramOther))]
        public string ProgramOther => ReadProperty(ProgramOtherProperty);
		public static readonly PropertyInfo<string> ProgramOtherProperty = RegisterProperty<string>(c => c.ProgramOther);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(FellowshipReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(FellowshipReadOnlyDto dto)
		{
            LoadProperty(IdProperty, dto.Id);
            LoadProperty(ProgramNameProperty, dto.ProgramName);
            LoadProperty(CompletionYearProperty, dto.CompletionYear);
            LoadProperty(ProgramOtherProperty, dto.ProgramOther);
		} 
        
    }
}
