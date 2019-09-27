using System.Collections.Generic;
using FizzBuzz.Framework.Autofac;
using FizzBuzz.Framework.Json.DependencyInjection;

namespace FizzBuzz.Framework.Mapping.DependencyInjection
{
	public class MappingContainer : AutofacContainer
	{
		protected override IEnumerable<ISimpleAutofacModule> Modules
		{
			get
			{
				yield return new JsonContainer();

				yield return new MappingModule();
			}
		}
	}
}
