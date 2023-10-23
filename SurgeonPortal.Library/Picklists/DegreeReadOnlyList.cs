using Csla;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Picklists.DegreeReadOnlyListFactory;

namespace SurgeonPortal.Library.Picklists
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class DegreeReadOnlyList : YtgReadOnlyListBase<IDegreeReadOnlyList, IDegreeReadOnly, int>, IDegreeReadOnlyList
    {
        private readonly IDegreeReadOnlyDal _degreeReadOnlyDal;

        public DegreeReadOnlyList(
            IIdentityProvider identityProvider,
            IDegreeReadOnlyDal degreeReadOnlyDal)
            : base(identityProvider)
        {
            _degreeReadOnlyDal = degreeReadOnlyDal;
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
        private async Task GetAll()
        
        {
            var dtos = await _degreeReadOnlyDal.GetAllAsync();
        			
            FetchChildren(dtos);
        }
    }
}
