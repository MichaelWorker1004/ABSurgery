using System;
using System.Reflection;
using Ytg.Framework.IoC;

namespace SurgeonPortal.Models
{

    public class ModelConventionResolver : IConventionResolver
	{
		#region IConventionResolver Implementation

		public Type GetImplementationType(Type serviceType)
		{
			var serviceAssemblyName = Assembly.GetAssembly(serviceType).FullName;
			var implementationAssembly = GetImplementationAssembly();

			// Remove the leading "I"
			var implementationName = serviceType.Name.Substring(1);

			var resolverType = GetResolverType();

			var serviceNamespaceParts = serviceType.Namespace.Split('.');
			var resolverNamespaceParts = resolverType.Namespace.Split('.');

			for (var i = 0; i < Math.Min(serviceNamespaceParts.Length, resolverNamespaceParts.Length); i++)
			{
				serviceNamespaceParts[i] = resolverNamespaceParts[i];
			}

			var implementationNamespace = string.Join(".", serviceNamespaceParts);
			implementationNamespace = implementationNamespace.Replace(".Contracts", string.Empty);

			var implementationType = Type.GetType($"{implementationNamespace}.{implementationName}Model, {implementationAssembly.GetName().Name}",
				x => implementationAssembly, typeResolver: null, throwOnError: false, ignoreCase: true);

			return implementationType;
		}

		#endregion

		public Assembly GetImplementationAssembly() => Assembly.GetAssembly(this.GetType());
		public Type GetResolverType() => this.GetType();
	}
}
