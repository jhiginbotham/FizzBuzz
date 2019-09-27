using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Core;

namespace FizzBuzz.Framework.Autofac
{
	public class OnInitiatingHandlerCollection : TypeEventHandlerCollection<ActivatingEventArgs<object>>
	{
		public override void RegisterEventHandler(IComponentRegistration registration)
		{
			var handler = GetHandlersForType(registration.Activator.LimitType);
			if (handler != null)
			{
				registration.Activating += handler;
			}
		}
	}
}
