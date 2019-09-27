using Autofac.Core;

namespace FizzBuzz.Framework.Autofac
{
	public class OnActivatedHandlerCollection : TypeEventHandlerCollection<ActivatedEventArgs<object>>
	{
		public override void RegisterEventHandler(IComponentRegistration registration)
		{
			var handler = GetHandlersForType(registration.Activator.LimitType);
			if (handler != null)
			{
				registration.Activated += handler;
			}
		}
	}
}
