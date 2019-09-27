using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Core;

namespace FizzBuzz.Framework.Autofac
{
	public abstract class TypeEventHandlerCollection<T> : Dictionary<Type, List<Action<T>>>, ITypeEventHandlerCollection
		where T : EventArgs
	{
		public void AddRegistration(TypeEventHandlerCollection<T> registrations)
		{
			foreach (var kvp in registrations)
			{
				if (!ContainsKey(kvp.Key))
				{
					Add(kvp.Key, kvp.Value);
				}
				else
				{
					this[kvp.Key].AddRange(kvp.Value);
				}
			}
		}

		public bool ContainsType(Type type)
		{
			foreach (var registrationType in Keys)
			{
				if (registrationType.IsAssignableFrom(type))
				{
					return true;
				}
			}

			return false;
		}

		public abstract void RegisterEventHandler(IComponentRegistration registration);

		protected EventHandler<T> GetHandlersForType(Type type)
		{
			var handlers = this.Where(x => x.Key.IsAssignableFrom(type)).SelectMany(s => s.Value).ToList();
			if (handlers.Any())
			{
				return CreateHandler(handlers);
			}

			return null;
		}

		private EventHandler<T> CreateHandler(List<Action<T>> handlers)
		{
			return (sender, args) =>
			{
				foreach (var handler in handlers)
				{
					handler(args);
				}
			};
		}
	}
}
