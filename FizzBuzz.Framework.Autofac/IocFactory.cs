namespace FizzBuzz.Framework.Autofac
{
	public class IocFactory : IIocFactory
	{
		public IIocContainer Create<T>() where T : IIocContainer, new()
		{
			return new T();
		}
	}
}
