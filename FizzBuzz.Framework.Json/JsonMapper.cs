using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FizzBuzz.Framework.Json
{
	public class JsonMapper : IJsonMapper
	{
		public T AutoMap<T>(object source)
		{
			if (source == null)
			{
				return default(T);
			}

			var obj = JObject.FromObject(source);
			return JsonConvert.DeserializeObject<T>(obj.ToString(Formatting.None));
		}

		public T AutoMap<T>(object source, T destination)
		{
			destination = AutoMap<T>(source);
			return destination;
		}

		public T Merge<T>(T origin, T content)
		{
			if (origin == null)
			{
				return content;
			}

			if (content == null)
			{
				return origin;
			}

			var obj1 = JObject.FromObject(origin);
			var obj2 = JObject.FromObject(content);

			obj1.Merge(obj2, new JsonMergeSettings
			{
				MergeArrayHandling = MergeArrayHandling.Union
			});

			return JsonConvert.DeserializeObject<T>(obj1.ToString(Formatting.None));
		}

		public T AutoMapMerge<T>(T origin, object source)
		{
			return Merge(origin, AutoMap<T>(source));
		}

		public T Clone<T>(T origin)
		{
			if (origin == null)
			{
				return default(T);
			}

			var obj = JObject.FromObject(origin);
			return JsonConvert.DeserializeObject<T>(obj.ToString(Formatting.None));
		}
	}
}
