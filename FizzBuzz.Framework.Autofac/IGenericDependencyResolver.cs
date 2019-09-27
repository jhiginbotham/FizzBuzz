namespace FizzBuzz.Framework.Autofac
{
	public interface IGenericDependencyResolver<TReturn>
	{
		TReturn Resolve<T>() where T : TReturn;
	}
}
