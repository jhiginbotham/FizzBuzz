using Autofac;
using Autofac.Core;

namespace FizzBuzz.Framework.Autofac
{
	internal class MultiEventRegistrationModule : Module
	{
		private readonly ITypeEventHandlerCollection _eventHandlerCollection;

		public MultiEventRegistrationModule
		(
			ITypeEventHandlerCollection eventHandlerCollection
		)
		{
			_eventHandlerCollection = eventHandlerCollection;
		}

		protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
		{
			_eventHandlerCollection.RegisterEventHandler(registration);
		}
	}
}
