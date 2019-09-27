using System;

namespace FizzBuzz.Framework.Autofac
{
	public interface IIocScope : IDisposable
	{
		bool TryResolve<T>(out T type);
		T Resolve<T>();
	}
}
