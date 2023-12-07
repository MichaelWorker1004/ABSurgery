using Csla;
using Csla.Rules;
using Microsoft.Extensions.Options;
using SurgeonPortal.Library.Contracts.Email;
using SurgeonPortal.Library.Users;
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
		[EmailAddress]
		public string To
		{
			get { return GetProperty(ToProperty); }
			set { SetProperty(ToProperty, value); }
		}
		public static readonly PropertyInfo<string> ToProperty = RegisterProperty<string>(c => c.To);

		[Required]
		[EmailAddress]
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

		public string TemplateId
		{
			get { return GetProperty(TemplateIdProperty); }
			set { SetProperty(TemplateIdProperty, value); }
		}
		public static readonly PropertyInfo<string> TemplateIdProperty = RegisterProperty<string>(c => c.TemplateId);

		public string PlainTextContent
		{
			get { return GetProperty(PlainTextContentProperty); }
			set { SetProperty(PlainTextContentProperty, value); }
		}
		public static readonly PropertyInfo<string> PlainTextContentProperty = RegisterProperty<string>(c => c.PlainTextContent);

		public IAttachment Attachment
		{
			get { return GetProperty(AttachmentProperty); }
			set { SetProperty(AttachmentProperty, value); }
		}
		public static readonly PropertyInfo<IAttachment> AttachmentProperty = RegisterProperty<IAttachment>(c => c.Attachment);

		protected override void AddBusinessRules()
		{
			base.AddBusinessRules();

			BusinessRules.AddRule(new EitherOrRequiredRule(TemplateIdProperty, PlainTextContentProperty, 1));
		}

		[Create]
		[RunLocal]
		private void Create()
		{
			base.DataPortal_Create();
			LoadProperty(FromProperty, _emailConfiguration.DefaultFrom);
		}

		public async Task SendAsync()
		{
			BusinessRules.CheckRules();
			if(!IsValid)
			{
				throw new Csla.Rules.ValidationException("Email is not valid to send.");
			}

			await _sendEmailCommandFactory.SendEmailCommandAsync(this);
		}
	}
}
