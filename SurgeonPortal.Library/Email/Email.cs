using Csla;
using Microsoft.Extensions.Options;
using SurgeonPortal.Library.Contracts.Email;
using SurgeonPortal.Shared.Email;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Email
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract]
	public class Email : YtgBusinessBase<Email>, IEmail
	{
		private readonly ISendEmailCommandFactory _sendEmailCommandFactory;
		private readonly EmailConfiguration _emailConfiguration;

		public Email(IIdentityProvider identity,
			ISendEmailCommandFactory sendEmailCommandFactory,
			IOptions<EmailConfiguration> emailConfiguration) : base(identity)
		{
			_sendEmailCommandFactory = sendEmailCommandFactory;
			_emailConfiguration = emailConfiguration.Value;
		}

		[Required]
		public string To
		{
			get { return GetProperty(ToProperty); }
			set { SetProperty(ToProperty, value); }
		}
		public static readonly PropertyInfo<string> ToProperty = RegisterProperty<string>(c => c.To);
		
		[Required]
		public string From
		{
			get { return GetProperty(FromProperty); }
			set { SetProperty(FromProperty, value); }
		}
		public static readonly PropertyInfo<string> FromProperty = RegisterProperty<string>(c => c.From);

		[Required]
		public string Subject
		{
			get { return GetProperty(SubjectProperty); }
			set { SetProperty(SubjectProperty, value); }
		}
		public static readonly PropertyInfo<string> SubjectProperty = RegisterProperty<string>(c => c.Subject);

		[Required]
		public string TemplateId
		{
			get { return GetProperty(TemplateIdProperty); }
			set { SetProperty(TemplateIdProperty, value); }
		}
		public static readonly PropertyInfo<string> TemplateIdProperty = RegisterProperty<string>(c => c.TemplateId);

		[Create]
		private void Create()
		{
			base.DataPortal_Create();
			LoadProperty(FromProperty, _emailConfiguration.DefaultFrom);
		}

		public async Task SendAsync()
		{
			await _sendEmailCommandFactory.SendEmailCommandAsync(this);
		}
	}
}
