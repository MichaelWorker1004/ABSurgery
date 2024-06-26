using Csla;
using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class RotationReadOnly : YtgReadOnlyBase<RotationReadOnly, int>, IRotationReadOnly
    {


        public RotationReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DataMember]
		[DisplayName(nameof(StartDate))]
        public DateTime StartDate => ReadProperty(StartDateProperty);
		public static readonly PropertyInfo<DateTime> StartDateProperty = RegisterProperty<DateTime>(c => c.StartDate);

        [DataMember]
		[DisplayName(nameof(EndDate))]
        public DateTime EndDate => ReadProperty(EndDateProperty);
		public static readonly PropertyInfo<DateTime> EndDateProperty = RegisterProperty<DateTime>(c => c.EndDate);

        [DataMember]
		[DisplayName(nameof(ProgramName))]
        public string ProgramName => ReadProperty(ProgramNameProperty);
		public static readonly PropertyInfo<string> ProgramNameProperty = RegisterProperty<string>(c => c.ProgramName);

        [DataMember]
		[DisplayName(nameof(AlternateInstitutionName))]
        public string AlternateInstitutionName => ReadProperty(AlternateInstitutionNameProperty);
		public static readonly PropertyInfo<string> AlternateInstitutionNameProperty = RegisterProperty<string>(c => c.AlternateInstitutionName);

        [DataMember]
		[DisplayName(nameof(ClinicalLevelId))]
        public int ClinicalLevelId => ReadProperty(ClinicalLevelIdProperty);
		public static readonly PropertyInfo<int> ClinicalLevelIdProperty = RegisterProperty<int>(c => c.ClinicalLevelId);

        [DataMember]
		[DisplayName(nameof(ClinicalLevel))]
        public string ClinicalLevel => ReadProperty(ClinicalLevelProperty);
		public static readonly PropertyInfo<string> ClinicalLevelProperty = RegisterProperty<string>(c => c.ClinicalLevel);

        [DataMember]
		[DisplayName(nameof(IsEssential))]
        public bool IsEssential => ReadProperty(IsEssentialProperty);
		public static readonly PropertyInfo<bool> IsEssentialProperty = RegisterProperty<bool>(c => c.IsEssential);

        [DataMember]
		[DisplayName(nameof(IsCredit))]
        public bool IsCredit => ReadProperty(IsCreditProperty);
		public static readonly PropertyInfo<bool> IsCreditProperty = RegisterProperty<bool>(c => c.IsCredit);

        [DataMember]
		[DisplayName(nameof(Other))]
        public string Other => ReadProperty(OtherProperty);
		public static readonly PropertyInfo<string> OtherProperty = RegisterProperty<string>(c => c.Other);

        [DataMember]
		[DisplayName(nameof(NonSurgicalActivity))]
        public string NonSurgicalActivity => ReadProperty(NonSurgicalActivityProperty);
		public static readonly PropertyInfo<string> NonSurgicalActivityProperty = RegisterProperty<string>(c => c.NonSurgicalActivity);

        [DataMember]
		[DisplayName(nameof(IsInternationalRotation))]
        public bool IsInternationalRotation => ReadProperty(IsInternationalRotationProperty);
		public static readonly PropertyInfo<bool> IsInternationalRotationProperty = RegisterProperty<bool>(c => c.IsInternationalRotation);

        [DataMember]
		[DisplayName(nameof(ClinicalActivity))]
        public string ClinicalActivity => ReadProperty(ClinicalActivityProperty);
		public static readonly PropertyInfo<string> ClinicalActivityProperty = RegisterProperty<string>(c => c.ClinicalActivity);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(RotationReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(RotationReadOnlyDto dto)
		{
            LoadProperty(IdProperty, dto.Id);
            LoadProperty(StartDateProperty, dto.StartDate);
            LoadProperty(EndDateProperty, dto.EndDate);
            LoadProperty(ProgramNameProperty, dto.ProgramName);
            LoadProperty(AlternateInstitutionNameProperty, dto.AlternateInstitutionName);
            LoadProperty(ClinicalLevelIdProperty, dto.ClinicalLevelId);
            LoadProperty(ClinicalLevelProperty, dto.ClinicalLevel);
            LoadProperty(IsEssentialProperty, dto.IsEssential);
            LoadProperty(IsCreditProperty, dto.IsCredit);
            LoadProperty(OtherProperty, dto.Other);
            LoadProperty(NonSurgicalActivityProperty, dto.NonSurgicalActivity);
            LoadProperty(IsInternationalRotationProperty, dto.IsInternationalRotation);
            LoadProperty(ClinicalActivityProperty, dto.ClinicalActivity);
		} 
        
    }
}
