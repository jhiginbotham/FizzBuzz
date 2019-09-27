using Autofac;

namespace FizzBuzz.Framework.Autofac
{
	public class AutofacScope : IIocScope
	{
		private ILifetimeScope _scope;
		public AutofacScope(ILifetimeScope scope)
		{
			_scope = scope;
		}

		public void Dispose()
		{
			_scope.Dispose();
		}

		public bool TryResolve<T>(out T type)
		{
			return _scope.TryResolve(out type);
		}

		public T Resolve<T>()
		{
			return _scope.Resolve<T>();
		}

	}
}
