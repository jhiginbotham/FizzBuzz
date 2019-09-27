using System;
using System.Collections.Generic;
using FizzBuzz.Business.FizzBuzz;
using FizzBuzz.Framework.Autofac.Testing;
using NUnit.Framework;

namespace FizzBuzz.DependencyInjection.Test
{
	[TestFixture]
	public class FizzBuzzContainerTest
	{
		private static IEnumerable<Type> TypesToResolve { get; } = DependencyInjectionAssert.GetResolvableTypes(typeof(IFizzBuzzService).Assembly);

		#region FizzBuzzContainer Tests

		[TestCaseSource(nameof(TypesToResolve))]
		public void FizzBuzzContainer_Should_Resolve_All_Types(Type typeToResolve)
		{
			DependencyInjectionAssert.CanResolveService<FizzBuzzContainer>(typeToResolve);
		}

		#endregion
	}
}
