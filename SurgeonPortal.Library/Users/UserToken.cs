using Csla;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Users.UserTokenFactory;

namespace SurgeonPortal.Library.Users
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class UserToken : YtgBusinessBase<UserToken>, IUserToken
    {
        private readonly IUserTokenDal _userTokenDal;

        public UserToken(
            IIdentityProvider identityProvider,
            IUserTokenDal userTokenDal)
            : base(identityProvider)
        {
            _userTokenDal = userTokenDal;
        }

        [DisplayName(nameof(UserId))]
		public int UserId
		{
			get { return GetProperty(UserIdProperty); }
			set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DisplayName(nameof(Token))]
		public string Token
		{
			get { return GetProperty(TokenProperty); }
			set { SetProperty(TokenProperty, value); }
		}
		public static readonly PropertyInfo<string> TokenProperty = RegisterProperty<string>(c => c.Token);

        [DisplayName(nameof(ExpiresAt))]
		public DateTime ExpiresAt
		{
			get { return GetProperty(ExpiresAtProperty); }
			set { SetProperty(ExpiresAtProperty, value); }
		}
		public static readonly PropertyInfo<DateTime> ExpiresAtProperty = RegisterProperty<DateTime>(c => c.ExpiresAt);



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
        private async Task GetActive(GetActiveCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _userTokenDal.GetActiveAsync(criteria.Token);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("UserToken not found based on criteria");
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
                var dto = await _userTokenDal.InsertAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(UserTokenDto dto)
		{
            base.FetchData(dto);
            
			this.UserId = dto.UserId;
			this.Token = dto.Token;
			this.ExpiresAt = dto.ExpiresAt;
		}

		internal UserTokenDto ToDto()
		{
			var dto = new UserTokenDto();
			return ToDto(dto);
		}
		internal UserTokenDto ToDto(UserTokenDto dto)
		{
            base.ToDto(dto);
            
			dto.UserId = this.UserId;
			dto.Token = this.Token;
			dto.ExpiresAt = this.ExpiresAt;

			return dto;
		}


    }
}
