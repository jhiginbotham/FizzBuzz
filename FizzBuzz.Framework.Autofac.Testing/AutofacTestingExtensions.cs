using Autofac;
using Autofac.Builder;
using Rhino.Mocks;

namespace FizzBuzz.Framework.Autofac.Testing
{
	public static class AutofacTestingExtensions
	{
		public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> RegisterMock<T>(this AutofacContainer container)
			where T : class
		{
			return container.Builder.Register(c => MockRepository.GenerateMock<T>()).AsSelf().SingleInstance();
		}
	}
}
