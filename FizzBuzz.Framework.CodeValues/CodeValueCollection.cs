using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FizzBuzz.Framework.CodeValues
{
	public class CodeValueCollection<T> : Dictionary<string, T> where T : CodeValue
	{
		public CodeValueCollection()
		{
			var staticFields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public)
				.Where(x => typeof(CodeValue).IsAssignableFrom(x.FieldType) && x.IsInitOnly).ToList();

			staticFields.ForEach(x => Add((T)x.GetValue(null)));
		}

		public void Add(T codeValue)
		{
			Add(codeValue.Code, codeValue);
		}

		public T GetCodeValue(string str)
		{
			if (string.IsNullOrWhiteSpace(str))
			{
				return default(T);
			}

			if (TryGetValue(str, out var value))
			{
				return value;
			}

			throw new InvalidCastException($"Failed to cast value '{str}' to CodeValue type {typeof(T).Name}.");
		}

		public T ForceGetCodeValue(string str)
		{
			if (string.IsNullOrWhiteSpace(str))
			{
				return default(T);
			}

			T value;

			if (!TryGetValue(str, out value))
			{
				value = (T)Activator.CreateInstance(typeof(T), str, "Unknown Type");
			}

			return value;
		}
	}
}
