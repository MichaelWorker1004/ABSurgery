using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
	public class PasswordResetCommandDto : YtgBusinessBaseDto
	{
		public int UserId { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
		public bool? PasswordReset { get; set; }
	}
}
