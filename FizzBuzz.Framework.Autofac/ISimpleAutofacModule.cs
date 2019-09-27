using Autofac;

namespace FizzBuzz.Framework.Autofac
{
	public interface ISimpleAutofacModule
	{
		void Load(ContainerBuilder builder);
	}
}
