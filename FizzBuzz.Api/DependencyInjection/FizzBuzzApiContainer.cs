using System.Collections.Generic;
using FizzBuzz.DependencyInjection;
using FizzBuzz.Framework.Autofac;
using FizzBuzz.Framework.Mapping.DependencyInjection;

namespace FizzBuzz.Api.DependencyInjection
{
	public class FizzBuzzApiContainer : AutofacContainer
	{
		protected override IEnumerable<ISimpleAutofacModule> Modules
		{
			get
			{
				yield return new MappingContainer();

				yield return new FizzBuzzContainer();
				yield return new FizzBuzzApiModule();
			}
		}
	}
}
