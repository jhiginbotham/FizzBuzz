using Autofac.Core;

namespace FizzBuzz.Framework.Autofac
{
	public interface ITypeEventHandlerCollection
	{
		void RegisterEventHandler(IComponentRegistration registration);
	}
}
