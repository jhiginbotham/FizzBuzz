using System;
using System.Collections.Generic;
using System.Linq;

namespace FizzBuzz.Framework.Autofac
{
	public class GenericDependencyResolver<TResult> : IGenericDependencyResolver<TResult>
	{
		private readonly IEnumerable<TResult> _registrations;

		public GenericDependencyResolver(IEnumerable<TResult> registrations)
		{
			_registrations = registrations;
		}

		public TResult Resolve<T>() where T : TResult
		{
			var items = _registrations.Where(x => x.GetType() == typeof(T)).ToList();

			if (items.Any() != true)
			{
				throw new ArgumentException("Unable to find proper registration for type: " + typeof(T));
			}

			if (items.Count > 1)
			{
				throw new ArgumentException("Multiple registrations found for type " + typeof(T));
			}

			return items.Single();
		}
	}
}
