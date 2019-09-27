namespace FizzBuzz.Framework.Json
{
	public class ObjectCloner : IObjectCloner
	{
		private readonly IJsonSerializer _jsonSerializer;

		public ObjectCloner(
			IJsonSerializer jsonSerializer
		)
		{
			_jsonSerializer = jsonSerializer;
		}

		public T DeepClone<T>(T item)
		{
			var str = _jsonSerializer.Serialize(item);
			return _jsonSerializer.Deserialize<T>(str);
		}
	}
}
