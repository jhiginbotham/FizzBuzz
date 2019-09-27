using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FizzBuzz.Framework.Json;

namespace FizzBuzz.Framework.Mapping
{
	public class TypeMapper : ITypeMapper
	{
		public ITypeMapper Mapper { get; set; }
		private readonly Dictionary<string, IConverter> _mappings = new Dictionary<string, IConverter>();
		private readonly IJsonMapper _jsonMapper = new JsonMapper();

		public T2 Map<T1, T2>(T1 source)
		{
			return Map(source, default(T2));
		}

		public T2 Map<T1, T2>(T1 source, T2 destination)
		{
			if (source == null)
			{
				return destination;
			}

			return MapConverter<T2>(typeof(T1), typeof(T2), source, destination);
		}

		public T Map<T>(object source)
		{
			return Map(source, default(T));
		}

		public T Map<T>(object source, T destination)
		{
			if (source == null)
			{
				return destination;
			}

			return MapConverter<T>(source.GetType(), typeof(T), source, destination);
		}

		public List<TDestination> MapList<TDestination>(IEnumerable source)
		{
			if (source == null)
			{
				return null;
			}

			var results = new List<TDestination>();
			foreach (var item in source)
			{
				results.Add(Map<TDestination>(item));
			}

			return results;
		}

		public List<TDestination> MapList<TDestination>(IEnumerable source, Type sourceType)
		{
			if (source == null)
			{
				return null;
			}

			var results = new List<TDestination>();
			foreach (var item in source)
			{
				results.Add(MapConverter<TDestination>(sourceType, typeof(TDestination), item, default(TDestination)));
			}

			return results;
		}

		private T MapConverter<T>(Type fromType, Type toType, object source, object destination)
		{
			try
			{
				return (T)GetConverter(fromType, toType).ConvertType(source, destination, Mapper ?? this, _jsonMapper);
			}
			catch (NotImplementedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new InvalidCastException("Failed to map to " + toType.FullName, ex);
			}
		}

		public bool HasMappingRegistered<TFrom, TTo>()
		{
			IConverter converter;
			return TryGetConverter(typeof(TFrom), typeof(TTo), out converter);
		}

		#region Registration Methods

		public ITypeMapper RegisterAssembly(Assembly mappingAssembly)
		{
			var modules = mappingAssembly.GetTypes().Where(t => t.IsClass && typeof(IMappingModule).IsAssignableFrom(t));
			foreach (var m in modules)
			{
				var module = (IMappingModule)Activator.CreateInstance(m);
				foreach (var converter in module.GetConverters())
				{
					Register(converter);
				}
			}

			return this;
		}

		private void Register(IConverter converter)
		{
			var hash = GetRegistrationHash(converter);

			if (_mappings.ContainsKey(hash))
			{
				throw new InvalidOperationException("Failed to register mapping. Duplicate mappings for " + hash);
			}

			_mappings.Add(hash, converter);
		}

		private IConverter GetConverter(Type fromType, Type toType)
		{
			IConverter converter;
			if (!TryGetConverter(fromType, toType, out converter))
			{
				throw new NotImplementedException("Unable to find valid mapper from type (" + fromType.FullName + ") to type (" + toType.FullName + ").");
			}

			return converter;
		}

		private bool TryGetConverter(Type fromType, Type toType, out IConverter converter)
		{
			converter = null;
			var hash = GetRegistrationHash(fromType, toType);
			if (_mappings.ContainsKey(hash))
			{
				converter = _mappings[hash];
				return true;
			}

			return false;
		}

		private string GetRegistrationHash(IConverter converter)
		{
			return GetRegistrationHash(converter.FromType, converter.ToType);
		}

		private string GetRegistrationHash(Type fromType, Type toType)
		{
			return fromType.FullName + " -> " + toType.FullName;
		}

		#endregion
	}
}
