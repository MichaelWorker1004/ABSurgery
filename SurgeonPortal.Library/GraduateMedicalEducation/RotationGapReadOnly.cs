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
    public class RotationGapReadOnly : YtgReadOnlyBase<RotationGapReadOnly, int>, IRotationGapReadOnly
    {


        public RotationGapReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(StartDate))]
        public DateTime? StartDate => ReadProperty(StartDateProperty);
		public static readonly PropertyInfo<DateTime?> StartDateProperty = RegisterProperty<DateTime?>(c => c.StartDate);

        [DataMember]
		[DisplayName(nameof(EndDate))]
        public DateTime? EndDate => ReadProperty(EndDateProperty);
		public static readonly PropertyInfo<DateTime?> EndDateProperty = RegisterProperty<DateTime?>(c => c.EndDate);

        [DataMember]
		[DisplayName(nameof(PreviousRotationId))]
        public int PreviousRotationId => ReadProperty(PreviousRotationIdProperty);
		public static readonly PropertyInfo<int> PreviousRotationIdProperty = RegisterProperty<int>(c => c.PreviousRotationId);

        [DataMember]
		[DisplayName(nameof(NextRotationId))]
        public int? NextRotationId => ReadProperty(NextRotationIdProperty);
		public static readonly PropertyInfo<int?> NextRotationIdProperty = RegisterProperty<int?>(c => c.NextRotationId);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(RotationGapReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(RotationGapReadOnlyDto dto)
		{
            LoadProperty(StartDateProperty, dto.StartDate);
            LoadProperty(EndDateProperty, dto.EndDate);
            LoadProperty(PreviousRotationIdProperty, dto.PreviousRotationId);
            LoadProperty(NextRotationIdProperty, dto.NextRotationId);
		} 
        
    }
}
