using Csla;
using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class GmeSummaryReadOnly : ReadOnlyBase<GmeSummaryReadOnly>, IGmeSummaryReadOnly
    {
        [DataMember]
		[DisplayName(nameof(ClinicalLevel))]
        public string ClinicalLevel => ReadProperty(ClinicalLevelProperty);
		public static readonly PropertyInfo<string> ClinicalLevelProperty = RegisterProperty<string>(c => c.ClinicalLevel);

        [DataMember]
		[DisplayName(nameof(MinStartDate))]
        public DateTime? MinStartDate => ReadProperty(MinStartDateProperty);
		public static readonly PropertyInfo<DateTime?> MinStartDateProperty = RegisterProperty<DateTime?>(c => c.MinStartDate);

        [DataMember]
		[DisplayName(nameof(MaxStartDate))]
        public DateTime? MaxStartDate => ReadProperty(MaxStartDateProperty);
		public static readonly PropertyInfo<DateTime?> MaxStartDateProperty = RegisterProperty<DateTime?>(c => c.MaxStartDate);

        [DataMember]
		[DisplayName(nameof(ProgramName))]
        public string ProgramName => ReadProperty(ProgramNameProperty);
		public static readonly PropertyInfo<string> ProgramNameProperty = RegisterProperty<string>(c => c.ProgramName);

        [DataMember]
		[DisplayName(nameof(ClinicalWeeks))]
        public int? ClinicalWeeks => ReadProperty(ClinicalWeeksProperty);
		public static readonly PropertyInfo<int?> ClinicalWeeksProperty = RegisterProperty<int?>(c => c.ClinicalWeeks);

        [DataMember]
		[DisplayName(nameof(NonClinicalWeeks))]
        public int? NonClinicalWeeks => ReadProperty(NonClinicalWeeksProperty);
		public static readonly PropertyInfo<int?> NonClinicalWeeksProperty = RegisterProperty<int?>(c => c.NonClinicalWeeks);

        [DataMember]
		[DisplayName(nameof(EssentialsWeeks))]
        public int? EssentialsWeeks => ReadProperty(EssentialsWeeksProperty);
		public static readonly PropertyInfo<int?> EssentialsWeeksProperty = RegisterProperty<int?>(c => c.EssentialsWeeks);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(GmeSummaryReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(GmeSummaryReadOnlyDto dto)
		{
            LoadProperty(ClinicalLevelProperty, dto.ClinicalLevel);
            LoadProperty(MinStartDateProperty, dto.MinStartDate);
            LoadProperty(MaxStartDateProperty, dto.MaxStartDate);
            LoadProperty(ProgramNameProperty, dto.ProgramName);
            LoadProperty(ClinicalWeeksProperty, dto.ClinicalWeeks);
            LoadProperty(NonClinicalWeeksProperty, dto.NonClinicalWeeks);
            LoadProperty(EssentialsWeeksProperty, dto.EssentialsWeeks);
		} 
        
    }
}
