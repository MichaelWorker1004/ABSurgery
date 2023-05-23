using Csla;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SurgeonPortal.Library.Picklists
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class GraduateProfileReadOnly : ReadOnlyBase<GraduateProfileReadOnly>, IGraduateProfileReadOnly
    {
        [DataMember]
		[DisplayName(nameof(Type))]
        public string Type => ReadProperty(TypeProperty);
		public static readonly PropertyInfo<string> TypeProperty = RegisterProperty<string>(c => c.Type);

        [DataMember]
		[DisplayName(nameof(Description))]
        public string Description => ReadProperty(DescriptionProperty);
		public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(GraduateProfileReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(GraduateProfileReadOnlyDto dto)
		{
            LoadProperty(TypeProperty, dto.Type);
            LoadProperty(DescriptionProperty, dto.Description);
		} 
        
    }
}
