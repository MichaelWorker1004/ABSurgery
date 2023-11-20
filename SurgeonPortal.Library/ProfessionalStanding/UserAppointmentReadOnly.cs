using Csla;
using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using SurgeonPortal.Library.Contracts.ProfessionalStanding;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.ProfessionalStanding
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class UserAppointmentReadOnly : YtgReadOnlyBase<UserAppointmentReadOnly, int>, IUserAppointmentReadOnly
    {


        public UserAppointmentReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(ApptId))]
        public decimal ApptId => ReadProperty(ApptIdProperty);
		public static readonly PropertyInfo<decimal> ApptIdProperty = RegisterProperty<decimal>(c => c.ApptId);

        [DataMember]
		[DisplayName(nameof(UserId))]
        public int? UserId => ReadProperty(UserIdProperty);
		public static readonly PropertyInfo<int?> UserIdProperty = RegisterProperty<int?>(c => c.UserId);

        [DataMember]
		[DisplayName(nameof(PracticeTypeId))]
        public int? PracticeTypeId => ReadProperty(PracticeTypeIdProperty);
		public static readonly PropertyInfo<int?> PracticeTypeIdProperty = RegisterProperty<int?>(c => c.PracticeTypeId);

        [DataMember]
		[DisplayName(nameof(PracticeType))]
        public string PracticeType => ReadProperty(PracticeTypeProperty);
		public static readonly PropertyInfo<string> PracticeTypeProperty = RegisterProperty<string>(c => c.PracticeType);

        [DataMember]
		[DisplayName(nameof(AppointmentTypeId))]
        public int? AppointmentTypeId => ReadProperty(AppointmentTypeIdProperty);
		public static readonly PropertyInfo<int?> AppointmentTypeIdProperty = RegisterProperty<int?>(c => c.AppointmentTypeId);

        [DataMember]
		[DisplayName(nameof(AppointmentType))]
        public string AppointmentType => ReadProperty(AppointmentTypeProperty);
		public static readonly PropertyInfo<string> AppointmentTypeProperty = RegisterProperty<string>(c => c.AppointmentType);

        [DataMember]
		[DisplayName(nameof(OrganizationTypeId))]
        public int? OrganizationTypeId => ReadProperty(OrganizationTypeIdProperty);
		public static readonly PropertyInfo<int?> OrganizationTypeIdProperty = RegisterProperty<int?>(c => c.OrganizationTypeId);

        [DataMember]
		[DisplayName(nameof(AuthorizingOfficial))]
        public string AuthorizingOfficial => ReadProperty(AuthorizingOfficialProperty);
		public static readonly PropertyInfo<string> AuthorizingOfficialProperty = RegisterProperty<string>(c => c.AuthorizingOfficial);

        [DataMember]
		[DisplayName(nameof(OrganizationType))]
        public string OrganizationType => ReadProperty(OrganizationTypeProperty);
		public static readonly PropertyInfo<string> OrganizationTypeProperty = RegisterProperty<string>(c => c.OrganizationType);

        [DataMember]
		[DisplayName(nameof(OrganizationId))]
        public int? OrganizationId => ReadProperty(OrganizationIdProperty);
		public static readonly PropertyInfo<int?> OrganizationIdProperty = RegisterProperty<int?>(c => c.OrganizationId);

        [DataMember]
		[DisplayName(nameof(StateCode))]
        public string StateCode => ReadProperty(StateCodeProperty);
		public static readonly PropertyInfo<string> StateCodeProperty = RegisterProperty<string>(c => c.StateCode);

        [DataMember]
		[DisplayName(nameof(Other))]
        public string Other => ReadProperty(OtherProperty);
		public static readonly PropertyInfo<string> OtherProperty = RegisterProperty<string>(c => c.Other);

        [DataMember]
		[DisplayName(nameof(OrganizationName))]
        public string OrganizationName => ReadProperty(OrganizationNameProperty);
		public static readonly PropertyInfo<string> OrganizationNameProperty = RegisterProperty<string>(c => c.OrganizationName);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(UserAppointmentReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(UserAppointmentReadOnlyDto dto)
		{
            LoadProperty(ApptIdProperty, dto.ApptId);
            LoadProperty(UserIdProperty, dto.UserId);
            LoadProperty(PracticeTypeIdProperty, dto.PracticeTypeId);
            LoadProperty(PracticeTypeProperty, dto.PracticeType);
            LoadProperty(AppointmentTypeIdProperty, dto.AppointmentTypeId);
            LoadProperty(AppointmentTypeProperty, dto.AppointmentType);
            LoadProperty(OrganizationTypeIdProperty, dto.OrganizationTypeId);
            LoadProperty(AuthorizingOfficialProperty, dto.AuthorizingOfficial);
            LoadProperty(OrganizationTypeProperty, dto.OrganizationType);
            LoadProperty(OrganizationIdProperty, dto.OrganizationId);
            LoadProperty(StateCodeProperty, dto.StateCode);
            LoadProperty(OtherProperty, dto.Other);
            LoadProperty(OrganizationNameProperty, dto.OrganizationName);
		} 
        
    }
}
