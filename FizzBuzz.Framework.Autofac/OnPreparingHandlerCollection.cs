using Autofac.Core;

namespace FizzBuzz.Framework.Autofac
{
	public class OnPreparingHandlerCollection : TypeEventHandlerCollection<PreparingEventArgs>
	{
		public override void RegisterEventHandler(IComponentRegistration registration)
		{
			var handler = GetHandlersForType(registration.Activator.LimitType);
			if (handler != null)
			{
				registration.Preparing += handler;
			}
		}
	}
}
