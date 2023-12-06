using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Email
{
	public interface IEmail
	{
		string To { get; set; }
		string From { get; set; }
		string Subject { get; set; }
		string TemplateId { get; set; }
		string PlainTextContent { get; set; }
		IAttachment Attachment { get; set; }
		Task SendAsync();
	}
}
