using Autofac;
using Autofac.Builder;

namespace FizzBuzz.Framework.Autofac.Extensions
{
	public static class AutofacContainerBuilderExtensions
	{
		public static IRegistrationBuilder<TConcrete, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddSelf<TConcrete>(this ContainerBuilder builder)
			where TConcrete : class
		{
			return builder.RegisterType<TConcrete>().AsSelf();
		}

		public static IRegistrationBuilder<TConcrete, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddType<TInterface, TConcrete>(this ContainerBuilder builder)
			where TInterface : class
			where TConcrete : class, TInterface
		{
			return builder.RegisterType<TConcrete>().As<TInterface>();
		}

		public static IRegistrationBuilder<TConcrete, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddSingleton<TConcrete>(this ContainerBuilder builder)
			where TConcrete : class
		{
			return builder.AddSelf<TConcrete>().SingleInstance();
		}

		public static IRegistrationBuilder<TConcrete, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddSingleton<TInterface, TConcrete>(this ContainerBuilder builder)
			where TInterface : class
			where TConcrete : class, TInterface
		{
			return builder.AddType<TInterface, TConcrete>().SingleInstance();
		}

		public static IRegistrationBuilder<TConcrete, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddInstancePerDependency<TConcrete>(this ContainerBuilder builder)
			where TConcrete : class
		{
			return builder.AddSelf<TConcrete>().InstancePerDependency();
		}

		public static IRegistrationBuilder<TConcrete, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddInstancePerDependency<TInterface, TConcrete>(this ContainerBuilder builder)
			where TInterface : class
			where TConcrete : class, TInterface
		{
			return builder.AddType<TInterface, TConcrete>().InstancePerDependency();
		}

		public static IRegistrationBuilder<TConcrete, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddTransient<TConcrete>(this ContainerBuilder builder)
			where TConcrete : class
		{
			return builder.AddSelf<TConcrete>().InstancePerDependency();
		}

		public static IRegistrationBuilder<TConcrete, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddTransient<TInterface, TConcrete>(this ContainerBuilder builder)
			where TInterface : class
			where TConcrete : class, TInterface
		{
			return builder.AddType<TInterface, TConcrete>().InstancePerDependency();
		}

		public static IRegistrationBuilder<TConcrete, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddScoped<TConcrete>(this ContainerBuilder builder)
			where TConcrete : class
		{
			return builder.AddSelf<TConcrete>().InstancePerLifetimeScope();
		}

		public static IRegistrationBuilder<TConcrete, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddScoped<TInterface, TConcrete>(this ContainerBuilder builder)
			where TInterface : class
			where TConcrete : class, TInterface
		{
			return builder.AddType<TInterface, TConcrete>().InstancePerLifetimeScope();
		}
	}
}
