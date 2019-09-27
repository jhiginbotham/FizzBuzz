namespace FizzBuzz.Framework.Autofac
{
	public interface IIocFactory
	{
		IIocContainer Create<T>() where T : IIocContainer, new();
	}
}
