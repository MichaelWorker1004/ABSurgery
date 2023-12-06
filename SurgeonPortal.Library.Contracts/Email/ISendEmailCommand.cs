namespace SurgeonPortal.Library.Contracts.Email
{
	public interface ISendEmailCommand
	{
		public IEmail Email { get; }
	}
}
