using Csla;
using SurgeonPortal.DataAccess.Contracts.Surgeons;
using SurgeonPortal.Library.Contracts.Surgeons;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using static SurgeonPortal.Library.Surgeons.CertificationReadOnlyListFactory;

namespace SurgeonPortal.Library.Surgeons
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
	public class CertificationReadOnlyList : YtgReadOnlyListBase<ICertificationReadOnlyList, ICertificationReadOnly>, ICertificationReadOnlyList
    {
        private readonly ICertificationReadOnlyDal _certificationReadOnlyDal;

        public CertificationReadOnlyList(ICertificationReadOnlyDal certificationReadOnlyDal)
        {
            _certificationReadOnlyDal = certificationReadOnlyDal;
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
            var dtos = await _certificationReadOnlyDal.GetByUserIdAsync();
        			
            FetchChildren(dtos);
        }
    }
}
