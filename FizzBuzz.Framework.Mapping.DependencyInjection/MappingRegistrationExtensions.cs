using System.Reflection;
using FizzBuzz.Framework.Autofac;

namespace FizzBuzz.Framework.Mapping.DependencyInjection
{
	public static class MappingRegistrationExtensions
	{
		public static void RegisterMappingAssembly<TMappingHook>(this AutofacModule module) => RegisterMappingAssembly(module, typeof(TMappingHook).Assembly);
		public static void RegisterMappingAssembly(this AutofacModule module, Assembly assembly)
		{
			module.RegisterOnInitiatingHandler<ITypeMapper>(args => args.Instance.RegisterAssembly(assembly));
		}
	}
}
