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
    public class ScoringSessionReadOnly : YtgReadOnlyBase<ScoringSessionReadOnly, int>, IScoringSessionReadOnly
    {


        public ScoringSessionReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(ExamSchedule))]
        public string ExamSchedule => ReadProperty(ExamScheduleProperty);
		public static readonly PropertyInfo<string> ExamScheduleProperty = RegisterProperty<string>(c => c.ExamSchedule);

        [DataMember]
		[DisplayName(nameof(Session1Id))]
        public int Session1Id => ReadProperty(Session1IdProperty);
		public static readonly PropertyInfo<int> Session1IdProperty = RegisterProperty<int>(c => c.Session1Id);

        [DataMember]
		[DisplayName(nameof(Session2Id))]
        public int? Session2Id => ReadProperty(Session2IdProperty);
		public static readonly PropertyInfo<int?> Session2IdProperty = RegisterProperty<int?>(c => c.Session2Id);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(ScoringSessionReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(ScoringSessionReadOnlyDto dto)
		{
            LoadProperty(ExamScheduleProperty, dto.ExamSchedule);
            LoadProperty(Session1IdProperty, dto.Session1Id);
            LoadProperty(Session2IdProperty, dto.Session2Id);
		} 
        
    }
}
