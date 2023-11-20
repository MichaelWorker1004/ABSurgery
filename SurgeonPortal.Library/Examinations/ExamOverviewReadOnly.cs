using Csla;
using SurgeonPortal.DataAccess.Contracts.Examinations;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Examinations
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class ExamOverviewReadOnly : YtgReadOnlyBase<ExamOverviewReadOnly, int>, IExamOverviewReadOnly
    {


        public ExamOverviewReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(ExamName))]
        public string ExamName => ReadProperty(ExamNameProperty);
		public static readonly PropertyInfo<string> ExamNameProperty = RegisterProperty<string>(c => c.ExamName);

        [DataMember]
		[DisplayName(nameof(RegOpenDate))]
        public DateTime? RegOpenDate => ReadProperty(RegOpenDateProperty);
		public static readonly PropertyInfo<DateTime?> RegOpenDateProperty = RegisterProperty<DateTime?>(c => c.RegOpenDate);

        [DataMember]
		[DisplayName(nameof(RegEndDate))]
        public DateTime? RegEndDate => ReadProperty(RegEndDateProperty);
		public static readonly PropertyInfo<DateTime?> RegEndDateProperty = RegisterProperty<DateTime?>(c => c.RegEndDate);

        [DataMember]
		[DisplayName(nameof(ExamStartDate))]
        public DateTime? ExamStartDate => ReadProperty(ExamStartDateProperty);
		public static readonly PropertyInfo<DateTime?> ExamStartDateProperty = RegisterProperty<DateTime?>(c => c.ExamStartDate);

        [DataMember]
		[DisplayName(nameof(ExamEndDate))]
        public DateTime? ExamEndDate => ReadProperty(ExamEndDateProperty);
		public static readonly PropertyInfo<DateTime?> ExamEndDateProperty = RegisterProperty<DateTime?>(c => c.ExamEndDate);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(ExamOverviewReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(ExamOverviewReadOnlyDto dto)
		{
            LoadProperty(ExamNameProperty, dto.ExamName);
            LoadProperty(RegOpenDateProperty, dto.RegOpenDate);
            LoadProperty(RegEndDateProperty, dto.RegEndDate);
            LoadProperty(ExamStartDateProperty, dto.ExamStartDate);
            LoadProperty(ExamEndDateProperty, dto.ExamEndDate);
		} 
        
    }
}
