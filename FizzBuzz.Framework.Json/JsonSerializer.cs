using System;
using System.Threading.Tasks;
using FizzBuzz.Framework.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FizzBuzz.Framework.Json
{
	/// <summary>
	/// A JSON serializer that omits default and null values.
	/// </summary>
	public class JsonSerializer : IJsonSerializer
	{
		private readonly JsonSerializerSettings _settings;

		public JsonSerializer() : this(new SerializerSettings { FormatOutput = false, IncludeNullValues = false, UseCamelCase = false })
		{

		}

		public JsonSerializer(SerializerSettings settings)
		{
			var jsonSettings = new JsonSerializerSettings
			{
				Converters =
				{
					new MinDateConverter(),
					new SecureStringConverter()
				},
				NullValueHandling = settings.IncludeNullValues ? NullValueHandling.Include : NullValueHandling.Ignore,
				Formatting = settings.FormatOutput ? Formatting.Indented : Formatting.None
			};

			if (settings.UseCamelCase)
			{
				jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			}

			_settings = jsonSettings;
		}

		#region Serialization

		public string Serialize(object obj)
		{
			return JsonConvert.SerializeObject(obj, _settings);
		}

		public string Serialize(object obj, Type type)
		{
			return JsonConvert.SerializeObject(obj, type, _settings);
		}

		public Task<string> SerializeAsync(object obj)
		{
			return Task.Factory.StartNew(() => Serialize(obj));
		}

		#endregion

		#region Deserialization

		public T Deserialize<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json, _settings);
		}

		public Task<T> DeserializeAsync<T>(string json)
		{
			return Task.Factory.StartNew(() => Deserialize<T>(json));
		}

		public object DeserializeObject(string json)
		{
			return JsonConvert.DeserializeObject(json);
		}

		public object DeserializeObject(string json, Type type)
		{
			return JsonConvert.DeserializeObject(json, type);
		}

		public Task<object> DeserializeObjectAsync(string json)
		{
			return Task.Factory.StartNew(() => DeserializeObject(json));
		}

		public Task<object> DeserializeObjectAsync(string json, Type type)
		{
			return Task.Factory.StartNew(() => DeserializeObject(json, type));
		}

		public T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
		{
			return JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
		}

		#endregion
	}
}
