using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Autofac;
using Autofac.Core;
using Autofac.Core.Lifetime;

namespace FizzBuzz.Framework.Autofac.Testing
{
	public static class DependencyInjectionAssert
	{
		private static readonly Dictionary<Type, object> _sampleValues = new Dictionary<Type, object>
		{
			{ typeof(string), "test" }
		};

		public static void RegisterSampleValue<T>(T value)
		{
			_sampleValues[typeof(T)] = value;
		}

		public static IEnumerable<Type> GetResolvableTypes(Assembly assembly)
		{
			return assembly.GetTypes().Where(IsResolvableType);
		}

		private static bool IsResolvableType(Type type)
		{
			return
				!IsCompilerGenerated(type) &&
				!IsStaticClass(type) &&
				!IsException(type) &&
				!IsAttribute(type) &&
				!IsConcreteTypeWithInterface(type) &&
				type.IsPublic &&
				!type.IsGenericTypeDefinition &&
				!type.IsEnum;

		}

		private static bool IsConcreteTypeWithInterface(Type type)
		{
			return !type.IsInterface && type.GetInterfaces().Any(i => i.Assembly == type.Assembly);
		}

		private static bool IsAttribute(Type type)
		{
			return typeof(Attribute).IsAssignableFrom(type);
		}

		private static bool IsException(Type type)
		{
			return typeof(Exception).IsAssignableFrom(type);
		}

		private static bool IsCompilerGenerated(Type type)
		{
			return type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any();
		}

		private static bool IsStaticClass(Type type)
		{
			return type.IsAbstract && type.IsSealed;
		}

		public static IEnumerable<Type> GetResolvableTypes(ISimpleAutofacModule module)
		{
			var builder = new ContainerBuilder();
			module.Load(builder);
			var container = builder.Build();

			return container.ComponentRegistry.Registrations.SelectMany(reg => reg.Services.OfType<IServiceWithType>().Select(svc => svc.ServiceType)).Where(typ => !typ.Namespace.StartsWith("Autofac"));
		}

		public static void CanResolveServicesInAssembly<TContainer>(Assembly assembly)
			where TContainer : AutofacContainer, new()
		{
			CanResolveServices<TContainer>(GetResolvableTypes(assembly));
		}

		public static void CanResolveService<TContainer>(Type type)
			where TContainer : AutofacContainer, new()
		{
			CanResolveService(new TContainer(), type);
		}

		public static void CanResolveServices<TContainer>(IEnumerable<Type> types) where TContainer : AutofacContainer, new()
		{
			var container = new TContainer();

			foreach (var type in types)
			{
				CanResolveService(container, type);
			}
		}

		public static void CanResolveService<TContainer>(TContainer container, Type type)
			where TContainer : AutofacContainer, new()
		{
			var instance = Resolve(container, type);

			foreach (var constructor in instance.GetType().GetConstructors())
			{
				var funcParams = constructor.GetParameters().Where(p => p.ParameterType.IsGenericType && p.ParameterType.GetGenericTypeDefinition().FullName.StartsWith("System.Func"));
				foreach (var funcParam in funcParams)
				{
					var funcType = funcParam.ParameterType;
					RunResolverFunc(container, funcType);
				}
			}

			if (instance is Delegate)
			{
				RunResolverFunc(container, type);
			}
		}

		private static object Resolve<TContainer>(TContainer container, Type type)
			where TContainer : AutofacContainer, new()
		{
			var scope = container.BeginScope((string)MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
			try
			{
				return scope.GetType().GetMethod(nameof(IIocScope.Resolve)).MakeGenericMethod(type).Invoke(scope, new object[0]);
			}
			catch (TargetInvocationException exception)
			{
				throw exception.InnerException;
			}

		}

		private static void RunResolverFunc<TContainer>(TContainer container, Type funcType)
			where TContainer : AutofacContainer, new()
		{
			var resolveParams = GetFuncResolveParams(funcType);

			var func = (Delegate)Resolve(container, funcType);
			try
			{
				func.DynamicInvoke(resolveParams.ToArray());
			}
			catch (TargetInvocationException exception)
			{
				throw exception.InnerException;
			}
		}

		private static List<object> GetFuncResolveParams(Type funcType)
		{
			var genericArguments = funcType.GetGenericArguments();
			var resolveParams = new List<object>();
			foreach (var resolveParamType in genericArguments.TakeWhile(x => x != genericArguments.Last()))
			{
				var value = GetSampleValue(resolveParamType);
				resolveParams.Add(value);
			}

			return resolveParams;
		}

		private static object GetSampleValue(Type resolveParamType)
		{
			if (_sampleValues.TryGetValue(resolveParamType, out var value))
			{
				return value;
			}

			return Activator.CreateInstance(resolveParamType);
		}

		public static IEnumerable<Type> GetImplementingTypes(Type genericInterface, params Assembly[] searchAssemblies)
		{
			if (searchAssemblies?.Any() != true)
			{
				searchAssemblies = new[] { genericInterface.Assembly };
			}

			return searchAssemblies.SelectMany(a => a.GetTypes())
				.SelectMany(t => t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterface));
		}
	}
}
