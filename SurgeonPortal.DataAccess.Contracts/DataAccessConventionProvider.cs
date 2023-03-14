using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ytg.Framework.IoC;

namespace SurgeonPortal.DataAccess.Contracts
{
    public class DataAccessConventionProvider : ConventionProviderBase
    {
        #region ConventionProviderBase Implementation

        public override Assembly GetAssembly() => Assembly.GetAssembly(this.GetType());
        public override IEnumerable<Type> GetServiceTypes()
        {
            return GetAllInterfaces()
                .Where(t => t.Name.EndsWith("Dal"));
        }

        #endregion
    }
}
