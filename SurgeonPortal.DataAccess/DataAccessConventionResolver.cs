using System;
using System.Reflection;
using Ytg.Framework.IoC;

namespace SurgeonPortal.DataAccess
{
    public class DataAccessConventionResolver : ConventionResolverBase
    {
        #region ConventionResolverBase Implementation

        public override Assembly GetImplementationAssembly() => Assembly.GetAssembly(this.GetType());
        public override Type GetResolverType() => this.GetType();

        #endregion
    }
}
