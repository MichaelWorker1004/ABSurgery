using Csla;
using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using SurgeonPortal.Library.Contracts.ContinuousCertification;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.ContinuousCertification.OutcomeRegistryFactory;

namespace SurgeonPortal.Library.ContinuousCertification
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class OutcomeRegistry : YtgBusinessBase<OutcomeRegistry>, IOutcomeRegistry
    {
        private readonly IOutcomeRegistryDal _outcomeRegistryDal;

        public OutcomeRegistry(
            IIdentityProvider identityProvider,
            IOutcomeRegistryDal outcomeRegistryDal)
            : base(identityProvider)
        {
            _outcomeRegistryDal = outcomeRegistryDal;
        }

        [DisplayName(nameof(SurgeonSpecificRegistry))]
		public bool SurgeonSpecificRegistry
		{
			get { return GetProperty(SurgeonSpecificRegistryProperty); }
			set { SetProperty(SurgeonSpecificRegistryProperty, value); }
		}
		public static readonly PropertyInfo<bool> SurgeonSpecificRegistryProperty = RegisterProperty<bool>(c => c.SurgeonSpecificRegistry);

        [DisplayName(nameof(RegistryComments))]
		public string RegistryComments
		{
			get { return GetProperty(RegistryCommentsProperty); }
			set { SetProperty(RegistryCommentsProperty, value); }
		}
		public static readonly PropertyInfo<string> RegistryCommentsProperty = RegisterProperty<string>(c => c.RegistryComments);

        [DisplayName(nameof(RegisteredWithACHQC))]
		public bool RegisteredWithACHQC
		{
			get { return GetProperty(RegisteredWithACHQCProperty); }
			set { SetProperty(RegisteredWithACHQCProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithACHQCProperty = RegisterProperty<bool>(c => c.RegisteredWithACHQC);

        [DisplayName(nameof(RegisteredWithCESQIP))]
		public bool RegisteredWithCESQIP
		{
			get { return GetProperty(RegisteredWithCESQIPProperty); }
			set { SetProperty(RegisteredWithCESQIPProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithCESQIPProperty = RegisterProperty<bool>(c => c.RegisteredWithCESQIP);

        [DisplayName(nameof(RegisteredWithMBSAQIP))]
		public bool RegisteredWithMBSAQIP
		{
			get { return GetProperty(RegisteredWithMBSAQIPProperty); }
			set { SetProperty(RegisteredWithMBSAQIPProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithMBSAQIPProperty = RegisterProperty<bool>(c => c.RegisteredWithMBSAQIP);

        [DisplayName(nameof(RegisteredWithABA))]
		public bool RegisteredWithABA
		{
			get { return GetProperty(RegisteredWithABAProperty); }
			set { SetProperty(RegisteredWithABAProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithABAProperty = RegisterProperty<bool>(c => c.RegisteredWithABA);

        [DisplayName(nameof(RegisteredWithASBS))]
		public bool RegisteredWithASBS
		{
			get { return GetProperty(RegisteredWithASBSProperty); }
			set { SetProperty(RegisteredWithASBSProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithASBSProperty = RegisterProperty<bool>(c => c.RegisteredWithASBS);

        [DisplayName(nameof(RegisteredWithStatewideCollaboratives))]
		public bool RegisteredWithStatewideCollaboratives
		{
			get { return GetProperty(RegisteredWithStatewideCollaborativesProperty); }
			set { SetProperty(RegisteredWithStatewideCollaborativesProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithStatewideCollaborativesProperty = RegisterProperty<bool>(c => c.RegisteredWithStatewideCollaboratives);

        [DisplayName(nameof(RegisteredWithABMS))]
		public bool RegisteredWithABMS
		{
			get { return GetProperty(RegisteredWithABMSProperty); }
			set { SetProperty(RegisteredWithABMSProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithABMSProperty = RegisterProperty<bool>(c => c.RegisteredWithABMS);

        [DisplayName(nameof(RegisteredWithNCDB))]
		public bool RegisteredWithNCDB
		{
			get { return GetProperty(RegisteredWithNCDBProperty); }
			set { SetProperty(RegisteredWithNCDBProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithNCDBProperty = RegisterProperty<bool>(c => c.RegisteredWithNCDB);

        [DisplayName(nameof(RegisteredWithRQRS))]
		public bool RegisteredWithRQRS
		{
			get { return GetProperty(RegisteredWithRQRSProperty); }
			set { SetProperty(RegisteredWithRQRSProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithRQRSProperty = RegisterProperty<bool>(c => c.RegisteredWithRQRS);

        [DisplayName(nameof(RegisteredWithNSQIP))]
		public bool RegisteredWithNSQIP
		{
			get { return GetProperty(RegisteredWithNSQIPProperty); }
			set { SetProperty(RegisteredWithNSQIPProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithNSQIPProperty = RegisterProperty<bool>(c => c.RegisteredWithNSQIP);

        [DisplayName(nameof(RegisteredWithNTDB))]
		public bool RegisteredWithNTDB
		{
			get { return GetProperty(RegisteredWithNTDBProperty); }
			set { SetProperty(RegisteredWithNTDBProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithNTDBProperty = RegisterProperty<bool>(c => c.RegisteredWithNTDB);

        [DisplayName(nameof(RegisteredWithSTS))]
		public bool RegisteredWithSTS
		{
			get { return GetProperty(RegisteredWithSTSProperty); }
			set { SetProperty(RegisteredWithSTSProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithSTSProperty = RegisterProperty<bool>(c => c.RegisteredWithSTS);

        [DisplayName(nameof(RegisteredWithTQIP))]
		public bool RegisteredWithTQIP
		{
			get { return GetProperty(RegisteredWithTQIPProperty); }
			set { SetProperty(RegisteredWithTQIPProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithTQIPProperty = RegisterProperty<bool>(c => c.RegisteredWithTQIP);

        [DisplayName(nameof(RegisteredWithUNOS))]
		public bool RegisteredWithUNOS
		{
			get { return GetProperty(RegisteredWithUNOSProperty); }
			set { SetProperty(RegisteredWithUNOSProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithUNOSProperty = RegisterProperty<bool>(c => c.RegisteredWithUNOS);

        [DisplayName(nameof(RegisteredWithNCDR))]
		public bool RegisteredWithNCDR
		{
			get { return GetProperty(RegisteredWithNCDRProperty); }
			set { SetProperty(RegisteredWithNCDRProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithNCDRProperty = RegisterProperty<bool>(c => c.RegisteredWithNCDR);

        [DisplayName(nameof(RegisteredWithSVS))]
		public bool RegisteredWithSVS
		{
			get { return GetProperty(RegisteredWithSVSProperty); }
			set { SetProperty(RegisteredWithSVSProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithSVSProperty = RegisterProperty<bool>(c => c.RegisteredWithSVS);

        [DisplayName(nameof(RegisteredWithELSO))]
		public bool RegisteredWithELSO
		{
			get { return GetProperty(RegisteredWithELSOProperty); }
			set { SetProperty(RegisteredWithELSOProperty, value); }
		}
		public static readonly PropertyInfo<bool> RegisteredWithELSOProperty = RegisterProperty<bool>(c => c.RegisteredWithELSO);

        [DisplayName(nameof(UserConfirmed))]
		public bool UserConfirmed
		{
			get { return GetProperty(UserConfirmedProperty); }
			set { SetProperty(UserConfirmedProperty, value); }
		}
		public static readonly PropertyInfo<bool> UserConfirmedProperty = RegisterProperty<bool>(c => c.UserConfirmed);

        [DisplayName(nameof(UserConfirmedDateUtc))]
		public DateTime UserConfirmedDateUtc
		{
			get { return GetProperty(UserConfirmedDateUtcProperty); }
			set { SetProperty(UserConfirmedDateUtcProperty, value); }
		}
		public static readonly PropertyInfo<DateTime> UserConfirmedDateUtcProperty = RegisterProperty<DateTime>(c => c.UserConfirmedDateUtc);

        [DisplayName(nameof(UserId))]
		public int UserId
		{
			get { return GetProperty(UserIdProperty); }
			set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            

        }




        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId(GetByUserIdCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _outcomeRegistryDal.GetByUserIdAsync(criteria.UserId);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("OutcomeRegistry not found based on criteria");
                }
                FetchData(dto);
            }
        }

        [RunLocal]
        [Insert]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task Insert()
        {
            base.DataPortal_Insert();
        
            using (BypassPropertyChecks)
            {
                var dto = await _outcomeRegistryDal.InsertAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }

        [RunLocal]
        [Update]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task Update()
        {
            base.DataPortal_Update();
        
            using (BypassPropertyChecks)
            {
                var dto = await _outcomeRegistryDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(OutcomeRegistryDto dto)
		{
            base.FetchData(dto);
            
			this.SurgeonSpecificRegistry = dto.SurgeonSpecificRegistry;
			this.RegistryComments = dto.RegistryComments;
			this.RegisteredWithACHQC = dto.RegisteredWithACHQC;
			this.RegisteredWithCESQIP = dto.RegisteredWithCESQIP;
			this.RegisteredWithMBSAQIP = dto.RegisteredWithMBSAQIP;
			this.RegisteredWithABA = dto.RegisteredWithABA;
			this.RegisteredWithASBS = dto.RegisteredWithASBS;
			this.RegisteredWithStatewideCollaboratives = dto.RegisteredWithStatewideCollaboratives;
			this.RegisteredWithABMS = dto.RegisteredWithABMS;
			this.RegisteredWithNCDB = dto.RegisteredWithNCDB;
			this.RegisteredWithRQRS = dto.RegisteredWithRQRS;
			this.RegisteredWithNSQIP = dto.RegisteredWithNSQIP;
			this.RegisteredWithNTDB = dto.RegisteredWithNTDB;
			this.RegisteredWithSTS = dto.RegisteredWithSTS;
			this.RegisteredWithTQIP = dto.RegisteredWithTQIP;
			this.RegisteredWithUNOS = dto.RegisteredWithUNOS;
			this.RegisteredWithNCDR = dto.RegisteredWithNCDR;
			this.RegisteredWithSVS = dto.RegisteredWithSVS;
			this.RegisteredWithELSO = dto.RegisteredWithELSO;
			this.UserConfirmed = dto.UserConfirmed;
			this.UserConfirmedDateUtc = dto.UserConfirmedDateUtc;
			this.UserId = dto.UserId;
		}

		internal OutcomeRegistryDto ToDto()
		{
			var dto = new OutcomeRegistryDto();
			return ToDto(dto);
		}
		internal OutcomeRegistryDto ToDto(OutcomeRegistryDto dto)
		{
            base.ToDto(dto);
            
			dto.SurgeonSpecificRegistry = this.SurgeonSpecificRegistry;
			dto.RegistryComments = this.RegistryComments;
			dto.RegisteredWithACHQC = this.RegisteredWithACHQC;
			dto.RegisteredWithCESQIP = this.RegisteredWithCESQIP;
			dto.RegisteredWithMBSAQIP = this.RegisteredWithMBSAQIP;
			dto.RegisteredWithABA = this.RegisteredWithABA;
			dto.RegisteredWithASBS = this.RegisteredWithASBS;
			dto.RegisteredWithStatewideCollaboratives = this.RegisteredWithStatewideCollaboratives;
			dto.RegisteredWithABMS = this.RegisteredWithABMS;
			dto.RegisteredWithNCDB = this.RegisteredWithNCDB;
			dto.RegisteredWithRQRS = this.RegisteredWithRQRS;
			dto.RegisteredWithNSQIP = this.RegisteredWithNSQIP;
			dto.RegisteredWithNTDB = this.RegisteredWithNTDB;
			dto.RegisteredWithSTS = this.RegisteredWithSTS;
			dto.RegisteredWithTQIP = this.RegisteredWithTQIP;
			dto.RegisteredWithUNOS = this.RegisteredWithUNOS;
			dto.RegisteredWithNCDR = this.RegisteredWithNCDR;
			dto.RegisteredWithSVS = this.RegisteredWithSVS;
			dto.RegisteredWithELSO = this.RegisteredWithELSO;
			dto.UserConfirmed = this.UserConfirmed;
			dto.UserConfirmedDateUtc = this.UserConfirmedDateUtc;
			dto.UserId = this.UserId;

			return dto;
		}


    }
}
