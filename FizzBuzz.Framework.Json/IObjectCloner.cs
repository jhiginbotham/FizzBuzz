namespace FizzBuzz.Framework.Json
{
	public interface IObjectCloner
	{
		T DeepClone<T>(T item);
	}
}
