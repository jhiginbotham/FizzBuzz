using Autofac;
using FizzBuzz.Framework.Autofac;

namespace FizzBuzz.Framework.Json.DependencyInjection
{
	internal class JsonModule : ISimpleAutofacModule
	{
		public void Load(ContainerBuilder builder)
		{
			builder.RegisterType<JsonSerializer>().As<IJsonSerializer>().InstancePerDependency();
			builder.RegisterType<JsonMapper>().As<IJsonMapper>().SingleInstance();
			builder.RegisterType<ObjectCloner>().As<IObjectCloner>().SingleInstance();
		}
	}
}
