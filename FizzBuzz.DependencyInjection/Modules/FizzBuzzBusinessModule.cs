using Autofac;
using FizzBuzz.Business.Implementation.FizzBuzz;
using FizzBuzz.Framework.Autofac;
using FizzBuzz.Framework.Mapping.DependencyInjection;
using FizzBuzz.Mapping;

namespace FizzBuzz.DependencyInjection.Modules
{
	public class FizzBuzzBusinessModule : AutofacModule
	{
		public override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(typeof(FizzBuzzService).Assembly).AsImplementedInterfaces().SingleInstance();

			this.RegisterMappingAssembly<FizzBuzzMappingHook>();
		}
	}
}
