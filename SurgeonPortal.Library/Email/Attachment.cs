using Csla;
using SurgeonPortal.Library.Contracts.Email;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Email
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract]
	public class Attachment : YtgBusinessBase<Attachment>, IAttachment
	{
		public Attachment(IIdentityProvider identity) : base(identity)
		{
		}

		[Required]
		public string Filename
		{
			get { return GetProperty(FilenameProperty); }
			set { SetProperty(FilenameProperty, value); }
		}
		public static readonly PropertyInfo<string> FilenameProperty = RegisterProperty<string>(c => c.Filename);

		[Required]
		public byte[] Content
		{
			get { return GetProperty(ContentProperty); }
			set { SetProperty(ContentProperty, value); }
		}
		public static readonly PropertyInfo<byte[]> ContentProperty = RegisterProperty<byte[]>(c => c.Content);

		public string ContentType
		{
			get { return GetProperty(ContentTypeProperty); }
			set { SetProperty(ContentTypeProperty, value); }
		}
		public static readonly PropertyInfo<string> ContentTypeProperty = RegisterProperty<string>(c => c.ContentType);
	}
}
