using Csla;
using SurgeonPortal.DataAccess.Contracts.Documents;
using SurgeonPortal.Library.Contracts.Documents;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Documents.DocumentReadOnlyListFactory;

namespace SurgeonPortal.Library.Documents
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class DocumentReadOnlyList : YtgReadOnlyListBase<IDocumentReadOnlyList, IDocumentReadOnly>, IDocumentReadOnlyList
    {
        private readonly IDocumentReadOnlyDal _documentReadOnlyDal;

        public DocumentReadOnlyList(IDocumentReadOnlyDal documentReadOnlyDal)
        {
            _documentReadOnlyDal = documentReadOnlyDal;
        }

        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            

        }

        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetByUserId()
        
        {
            var dtos = await _documentReadOnlyDal.GetByUserIdAsync();
        			
            FetchChildren(dtos);
        }
    }
}
