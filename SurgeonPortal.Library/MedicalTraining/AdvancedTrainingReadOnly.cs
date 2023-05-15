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
    public class AdvancedTrainingReadOnly : ReadOnlyBase<AdvancedTrainingReadOnly>, IAdvancedTrainingReadOnly
    {
        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DataMember]
		[DisplayName(nameof(TrainingType))]
        public string TrainingType => ReadProperty(TrainingTypeProperty);
		public static readonly PropertyInfo<string> TrainingTypeProperty = RegisterProperty<string>(c => c.TrainingType);

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

        [DataMember]
		[DisplayName(nameof(Other))]
        public string Other => ReadProperty(OtherProperty);
		public static readonly PropertyInfo<string> OtherProperty = RegisterProperty<string>(c => c.Other);

        [DataMember]
		[DisplayName(nameof(StartDate))]
        public DateTime StartDate => ReadProperty(StartDateProperty);
		public static readonly PropertyInfo<DateTime> StartDateProperty = RegisterProperty<DateTime>(c => c.StartDate);

        [DataMember]
		[DisplayName(nameof(EndDate))]
        public DateTime EndDate => ReadProperty(EndDateProperty);
		public static readonly PropertyInfo<DateTime> EndDateProperty = RegisterProperty<DateTime>(c => c.EndDate);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(AdvancedTrainingReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(AdvancedTrainingReadOnlyDto dto)
		{
            LoadProperty(IdProperty, dto.Id);
            LoadProperty(TrainingTypeProperty, dto.TrainingType);
            LoadProperty(InstitutionNameProperty, dto.InstitutionName);
            LoadProperty(CityProperty, dto.City);
            LoadProperty(StateProperty, dto.State);
            LoadProperty(OtherProperty, dto.Other);
            LoadProperty(StartDateProperty, dto.StartDate);
            LoadProperty(EndDateProperty, dto.EndDate);
		} 
        
    }
}
