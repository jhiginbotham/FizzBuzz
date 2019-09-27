using System;
using System.Collections.Generic;
using FizzBuzz.Framework.Autofac;

namespace FizzBuzz.Framework.Json.DependencyInjection
{
	public class JsonContainer : AutofacContainer
	{
		protected override IEnumerable<ISimpleAutofacModule> Modules
		{
			get
			{
				yield return new JsonModule();
			}
		}
	}
}
