using System;
using System.Collections.Generic;
using System.Text;
using FizzBuzz.Framework.Json;

namespace FizzBuzz.Framework.Mapping
{
	public interface IConverter
	{
		Type FromType { get; }
		Type ToType { get; }
		string FromTypeName { get; }
		string ToTypeName { get; }
		object ConvertType(object source, object destination, ITypeMapper mapper);
		object ConvertType(object source, object destination, ITypeMapper mapper, IJsonMapper jsonMapper);
	}
}
