using Csla;
using SurgeonPortal.DataAccess.Contracts.Picklists;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Picklists.StateReadOnlyListFactory;

namespace SurgeonPortal.Library.Picklists
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class StateReadOnlyList : YtgReadOnlyListBase<IStateReadOnlyList, IStateReadOnly>, IStateReadOnlyList
    {
        private readonly IStateReadOnlyDal _stateReadOnlyDal;

        public StateReadOnlyList(IStateReadOnlyDal stateReadOnlyDal)
        {
            _stateReadOnlyDal = stateReadOnlyDal;
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
        private async Task GetByCountry(GetByCountryCriteria criteria)
        
        {
            var dtos = await _stateReadOnlyDal.GetByCountryAsync(criteria.CountryCode);
        			
            FetchChildren(dtos);
        }
    }
}
