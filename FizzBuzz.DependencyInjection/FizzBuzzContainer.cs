using System.Collections.Generic;
using FizzBuzz.DependencyInjection.Modules;
using FizzBuzz.Framework.Autofac;
using FizzBuzz.Framework.Mapping.DependencyInjection;

namespace FizzBuzz.DependencyInjection
{
	public class FizzBuzzContainer : AutofacContainer
	{
		protected override IEnumerable<ISimpleAutofacModule> Modules
		{
			get
			{
				yield return new MappingContainer();

				yield return new FizzBuzzBusinessModule();
			}
		}
	}
}
