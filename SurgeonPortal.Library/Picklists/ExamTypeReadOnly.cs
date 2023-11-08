using Csla;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Picklists
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class ExamTypeReadOnly : YtgReadOnlyBase<ExamTypeReadOnly, int>, IExamTypeReadOnly
    {


        public ExamTypeReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(Id))]
        public int Id => ReadProperty(IdProperty);
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DataMember]
		[DisplayName(nameof(Code))]
        public string Code => ReadProperty(CodeProperty);
		public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(c => c.Code);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(ExamTypeReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(ExamTypeReadOnlyDto dto)
		{
            LoadProperty(IdProperty, dto.Id);
            LoadProperty(CodeProperty, dto.Code);
		} 
        
    }
}
