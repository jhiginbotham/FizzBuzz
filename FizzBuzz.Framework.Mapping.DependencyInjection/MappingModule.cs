using System;
using Autofac;
using FizzBuzz.Framework.Autofac;

namespace FizzBuzz.Framework.Mapping.DependencyInjection
{
	internal class MappingModule : ISimpleAutofacModule
	{
		public void Load(ContainerBuilder builder)
		{
			builder.RegisterType<TypeMapper>().As<ITypeMapper>().SingleInstance().OnActivated(e => e.Instance.Mapper = e.Instance);
		}
	}
}
