namespace FizzBuzz.Framework.Json
{
	public interface IJsonMapper
	{
		T AutoMap<T>(object source);
		T AutoMap<T>(object source, T destination);
		T Merge<T>(T origin, T content);
		T AutoMapMerge<T>(T origin, object source);
		T Clone<T>(T origin);
	}
}
