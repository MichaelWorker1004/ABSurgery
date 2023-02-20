using System;
using System.Collections.Generic;
using System.Reflection;
using Ytg.Framework.IoC;

namespace SurgeonPortal.Library.Contracts
{
    public class LibraryConventionProvider : ConventionProviderBase
    {
        #region ConventionProviderBase Implementation

        public override Assembly GetAssembly() => Assembly.GetAssembly(this.GetType());
        public override IEnumerable<Type> GetServiceTypes()
        {
            return GetAllInterfaces();
        }

        #endregion
    }
}
