using Csla;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Picklists.OrganizationTypeReadOnlyListFactory;

namespace SurgeonPortal.Library.Picklists
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class OrganizationTypeReadOnlyList : YtgReadOnlyListBase<IOrganizationTypeReadOnlyList, IOrganizationTypeReadOnly, int>, IOrganizationTypeReadOnlyList
    {
        private readonly IOrganizationTypeReadOnlyDal _organizationTypeReadOnlyDal;

        public OrganizationTypeReadOnlyList(
            IIdentityProvider identityProvider,
            IOrganizationTypeReadOnlyDal organizationTypeReadOnlyDal)
            : base(identityProvider)
        {
            _organizationTypeReadOnlyDal = organizationTypeReadOnlyDal;
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
            var dtos = await _organizationTypeReadOnlyDal.GetAllAsync();
        			
            FetchChildren(dtos);
        }
    }
}
