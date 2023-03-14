using System;
using System.Reflection;
using Ytg.Framework.IoC;

namespace SurgeonPortal.Library
{
    public class LibraryConventionResolver : ConventionResolverBase
    {
        #region ConventionResolverBase Implementation

        public override Assembly GetImplementationAssembly() => Assembly.GetAssembly(this.GetType());
        public override Type GetResolverType() => this.GetType();

        #endregion
    }
}
