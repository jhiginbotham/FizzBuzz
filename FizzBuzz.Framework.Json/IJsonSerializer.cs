using System;
using System.Threading.Tasks;

namespace FizzBuzz.Framework.Json
{
	public interface IJsonSerializer
	{
		/// <summary>
		/// Serializes the given object to JSON. The result is minified by default.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>A JSON string representation of the object.</returns>
		string Serialize(object obj);

		/// <summary>
		/// Serializes the specified object to a JSON string using a type.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		/// <param name="type">The type of the value being serialized. 
		/// This parameter is used when TypeNameHandling is Auto to write out the type name if the type of the value does not match. Specifing the type is optional.</param>
		/// <returns>A JSON string representation of the object.</returns>
		string Serialize(object obj, Type type);

		/// <summary>
		/// Asynchronously serializes the specified object to a JSON string. Serialization will happen on a new thread.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>A task that represents the asynchronous serialize operation. The value of the TResult parameter contains a JSON string representation of the object.</returns>
		Task<string> SerializeAsync(object obj);

		/// <summary>
		/// Deserializes the given JSON string to the specified type <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type represented by the JSON string.</typeparam>
		/// <param name="json">The JSON to deserialize into <typeparamref name="T"/></param>
		/// <returns>The object representation of the JSON string.</returns>
		T Deserialize<T>(string json);

		/// <summary>
		/// Asynchronously deserializes the JSON to the specified .NET type. Deserialization will happen on a new thread.
		/// </summary>
		/// <typeparam name="T">The type of the object to deserialize to.</typeparam>
		/// <param name="json">The JSON to deserialize.</param>
		/// <returns>A task that represents the asynchronous deserialize operation. The value of the TResult parameter contains the deserialized object from the JSON string.</returns>
		Task<T> DeserializeAsync<T>(string json);

		/// <summary>
		/// Deserializes the JSON to a .NET object.
		/// </summary>
		/// <param name="json">The JSON to deserialize.</param>
		/// <returns>The deserialized object from the JSON string.</returns>
		object DeserializeObject(string json);

		/// <summary>
		/// Deserializes the JSON to the specified .NET type.
		/// </summary>
		/// <param name="json">The JSON to deserialize.</param>
		/// <param name="type">The System.Type of object being deserialized</param>
		/// <returns>The deserialized object from the JSON string.</returns>
		object DeserializeObject(string json, Type type);

		/// <summary>
		/// Asynchronously deserializes the JSON to the specified .NET type. Deserialization will happen on a new thread.
		/// </summary>
		/// <param name="json">The JSON to deserialize.</param>
		/// <returns>A task that represents the asynchronous deserialize operation. The value of the TResult parameter contains the deserialized object from the JSON string.</returns>
		Task<object> DeserializeObjectAsync(string json);

		/// <summary>
		/// Asynchronously deserializes the JSON to the specified .NET type. Deserialization will happen on a new thread.
		/// </summary>
		/// <param name="json">The JSON to deserialize.</param>
		/// <param name="type">The System.Type of object being deserialized</param>
		/// <returns>A task that represents the asynchronous deserialize operation. The value of the TResult parameter contains the deserialized object from the JSON string.</returns>
		Task<object> DeserializeObjectAsync(string json, Type type);

		/// <summary>
		/// Deserializes the JSON to the given anonymous type.
		/// </summary>
		/// <typeparam name="T">The anonymous type to deserialize to. This can't be specified traditionally and must be infered from the anonymous type passed as a parameter.</typeparam>
		/// <param name="json">The JSON to deserialize.</param>
		/// <param name="anonymousTypeObject">The anonymous type object.</param>
		/// <returns>The JSON to deserialize.</returns>
		T DeserializeAnonymousType<T>(string json, T anonymousTypeObject);
	}
}
