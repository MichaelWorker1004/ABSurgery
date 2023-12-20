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
    public class QeExamEligibilityReadOnly : YtgReadOnlyBase<QeExamEligibilityReadOnly, int>, IQeExamEligibilityReadOnly
    {


        public QeExamEligibilityReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(ExamId))]
        public int? ExamId => ReadProperty(ExamIdProperty);
		public static readonly PropertyInfo<int?> ExamIdProperty = RegisterProperty<int?>(c => c.ExamId);

        [DataMember]
		[DisplayName(nameof(ExamCode))]
        public string ExamCode => ReadProperty(ExamCodeProperty);
		public static readonly PropertyInfo<string> ExamCodeProperty = RegisterProperty<string>(c => c.ExamCode);

        [DataMember]
		[DisplayName(nameof(ExamName))]
        public string ExamName => ReadProperty(ExamNameProperty);
		public static readonly PropertyInfo<string> ExamNameProperty = RegisterProperty<string>(c => c.ExamName);

        [DataMember]
		[DisplayName(nameof(AppOpenDate))]
        public DateTime? AppOpenDate => ReadProperty(AppOpenDateProperty);
		public static readonly PropertyInfo<DateTime?> AppOpenDateProperty = RegisterProperty<DateTime?>(c => c.AppOpenDate);

        [DataMember]
		[DisplayName(nameof(AppCloseDate))]
        public DateTime? AppCloseDate => ReadProperty(AppCloseDateProperty);
		public static readonly PropertyInfo<DateTime?> AppCloseDateProperty = RegisterProperty<DateTime?>(c => c.AppCloseDate);

        [DataMember]
		[DisplayName(nameof(AppLateDate))]
        public DateTime? AppLateDate => ReadProperty(AppLateDateProperty);
		public static readonly PropertyInfo<DateTime?> AppLateDateProperty = RegisterProperty<DateTime?>(c => c.AppLateDate);

        [DataMember]
		[DisplayName(nameof(AppDelayDate))]
        public DateTime? AppDelayDate => ReadProperty(AppDelayDateProperty);
		public static readonly PropertyInfo<DateTime?> AppDelayDateProperty = RegisterProperty<DateTime?>(c => c.AppDelayDate);

        [DataMember]
		[DisplayName(nameof(ExamStartDate))]
        public DateTime? ExamStartDate => ReadProperty(ExamStartDateProperty);
		public static readonly PropertyInfo<DateTime?> ExamStartDateProperty = RegisterProperty<DateTime?>(c => c.ExamStartDate);

        [DataMember]
		[DisplayName(nameof(ExamEndDate))]
        public DateTime? ExamEndDate => ReadProperty(ExamEndDateProperty);
		public static readonly PropertyInfo<DateTime?> ExamEndDateProperty = RegisterProperty<DateTime?>(c => c.ExamEndDate);

        [DataMember]
		[DisplayName(nameof(ApplicationApproved))]
        public int ApplicationApproved => ReadProperty(ApplicationApprovedProperty);
		public static readonly PropertyInfo<int> ApplicationApprovedProperty = RegisterProperty<int>(c => c.ApplicationApproved);

        [DataMember]
		[DisplayName(nameof(RegistrationOpen))]
        public int RegistrationOpen => ReadProperty(RegistrationOpenProperty);
		public static readonly PropertyInfo<int> RegistrationOpenProperty = RegisterProperty<int>(c => c.RegistrationOpen);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(QeExamEligibilityReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(QeExamEligibilityReadOnlyDto dto)
		{
            LoadProperty(ExamIdProperty, dto.ExamId);
            LoadProperty(ExamCodeProperty, dto.ExamCode);
            LoadProperty(ExamNameProperty, dto.ExamName);
            LoadProperty(AppOpenDateProperty, dto.AppOpenDate);
            LoadProperty(AppCloseDateProperty, dto.AppCloseDate);
            LoadProperty(AppLateDateProperty, dto.AppLateDate);
            LoadProperty(AppDelayDateProperty, dto.AppDelayDate);
            LoadProperty(ExamStartDateProperty, dto.ExamStartDate);
            LoadProperty(ExamEndDateProperty, dto.ExamEndDate);
            LoadProperty(ApplicationApprovedProperty, dto.ApplicationApproved);
            LoadProperty(RegistrationOpenProperty, dto.RegistrationOpen);
		} 
        
    }
}
