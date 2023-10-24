using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations.GQ;
using SurgeonPortal.Library.Contracts.Examinations.GQ;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Examinations.GQ
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class AdditionalTrainingReadOnly : YtgReadOnlyBase<AdditionalTrainingReadOnly, int>, IAdditionalTrainingReadOnly
    {


        public AdditionalTrainingReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(TrainingId))]
        public int TrainingId => ReadProperty(TrainingIdProperty);
		public static readonly PropertyInfo<int> TrainingIdProperty = RegisterProperty<int>(c => c.TrainingId);

        [DataMember]
		[DisplayName(nameof(TypeOfTraining))]
        public string TypeOfTraining => ReadProperty(TypeOfTrainingProperty);
		public static readonly PropertyInfo<string> TypeOfTrainingProperty = RegisterProperty<string>(c => c.TypeOfTraining);

        [DataMember]
		[DisplayName(nameof(State))]
        public string State => ReadProperty(StateProperty);
		public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(c => c.State);

        [DataMember]
		[DisplayName(nameof(City))]
        public string City => ReadProperty(CityProperty);
		public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(c => c.City);

        [DataMember]
		[DisplayName(nameof(InstitutionName))]
        public string InstitutionName => ReadProperty(InstitutionNameProperty);
		public static readonly PropertyInfo<string> InstitutionNameProperty = RegisterProperty<string>(c => c.InstitutionName);

        [DataMember]
		[DisplayName(nameof(Other))]
        public string Other => ReadProperty(OtherProperty);
		public static readonly PropertyInfo<string> OtherProperty = RegisterProperty<string>(c => c.Other);

        [DataMember]
		[DisplayName(nameof(DateStarted))]
        public DateTime DateStarted => ReadProperty(DateStartedProperty);
		public static readonly PropertyInfo<DateTime> DateStartedProperty = RegisterProperty<DateTime>(c => c.DateStarted);

        [DataMember]
		[DisplayName(nameof(DateEnded))]
        public DateTime DateEnded => ReadProperty(DateEndedProperty);
		public static readonly PropertyInfo<DateTime> DateEndedProperty = RegisterProperty<DateTime>(c => c.DateEnded);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(AdditionalTrainingReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(AdditionalTrainingReadOnlyDto dto)
		{
            LoadProperty(TrainingIdProperty, dto.TrainingId);
            LoadProperty(TypeOfTrainingProperty, dto.TypeOfTraining);
            LoadProperty(StateProperty, dto.State);
            LoadProperty(CityProperty, dto.City);
            LoadProperty(InstitutionNameProperty, dto.InstitutionName);
            LoadProperty(OtherProperty, dto.Other);
            LoadProperty(DateStartedProperty, dto.DateStarted);
            LoadProperty(DateEndedProperty, dto.DateEnded);
		} 
        
    }
}
