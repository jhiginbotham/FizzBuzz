using System;
using FizzBuzz.Framework.Json;

namespace FizzBuzz.Framework.Mapping
{
	public abstract class BaseConverter<TFromType, TToType> : IConverter
	{
		public Type FromType { get { return typeof(TFromType); } }
		public Type ToType { get { return typeof(TToType); } }
		public string FromTypeName { get { return typeof(TFromType).FullName; } }
		public string ToTypeName { get { return typeof(TToType).FullName; } }

		private IJsonMapper _jsonMapper;

		public abstract TToType ConvertType(TFromType source, TToType destination, ITypeMapper mapper);

		public object ConvertType(object source, object destination, ITypeMapper mapper)
		{
			if (!(source is TFromType) || (destination != null && !(destination is TToType)))
			{
				return default(TToType);
			}

			TToType dest = (TToType)destination;
			if (dest == null)
			{
				var constructor = typeof(TToType).GetConstructor(Type.EmptyTypes);
				if (constructor != null)
				{
					dest = Activator.CreateInstance<TToType>();
				}
				else
				{
					dest = default(TToType);
				}
			}

			return ConvertType((TFromType)source, dest, mapper);
		}

		public object ConvertType(object source, object destination, ITypeMapper mapper, IJsonMapper jsonMapper)
		{
			_jsonMapper = jsonMapper;
			return ConvertType(source, destination, mapper);
		}

		#region Json Mapping Methods

		protected T AutoMap<T>(object source)
		{
			return _jsonMapper.AutoMap<T>(source);
		}

		protected T Merge<T>(T origin, T content)
		{
			return _jsonMapper.Merge(origin, content);
		}

		protected T Clone<T>(T obj)
		{
			return _jsonMapper.Clone(obj);
		}

		#endregion
	}
}
