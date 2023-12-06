namespace SurgeonPortal.Library.Contracts.EmailProvider
{
	public class TemplateEmailResource : EmailResourceBase
	{
		public string Template { get; set; }
		public object TemplateData { get; set; }
	}
}
