namespace FizzBuzz.Framework.Autofac
{
	public interface IIocContainer : ISimpleAutofacModule, IIocScope
	{
		IIocScope BeginScope();
	}
}
