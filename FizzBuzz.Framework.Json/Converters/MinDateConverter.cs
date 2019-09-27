using System;
using Newtonsoft.Json;

namespace FizzBuzz.Framework.Json.Converters
{
	public class MinDateConverter : JsonConverter
	{
		public override bool CanRead { get { return false; } }

		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(DateTime));
		}

		public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
		{
			DateTime date = (DateTime)value;
			if (date != DateTime.MinValue)
			{
				writer.WriteValue(date);
			}
			else
			{
				writer.WriteNull();
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
		{
			throw new NotImplementedException("Converter will be skipped because CanRead is false.");
		}
	}
}
