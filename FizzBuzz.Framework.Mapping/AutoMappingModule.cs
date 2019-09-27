using System;
using System.Collections.Generic;
using System.Text;

namespace FizzBuzz.Framework.Mapping
{
	public abstract class AutoMappingModule : IMappingModule
	{
		private readonly List<IConverter> _converters = new List<IConverter>();

		public abstract void WireAutoMapConverters();

		public IEnumerable<IConverter> GetConverters()
		{
			WireAutoMapConverters();

			return _converters;
		}

		/// <summary>
		/// Registers one way AutoMapConverter to support single directional mapping from <see cref="FromType"/> to <see cref="ToType"/>
		/// </summary>
		/// <typeparam name="FromType">Type that can be mapped to resulting type <see cref="ToType"/></typeparam>
		/// <typeparam name="ToType">Resulting type.</typeparam>
		/// <param name="merge">If <value>true</value> source object will merge with destination; otherwise will replace entire object.</param>
		public void RegisterOneWayMapping<FromType, ToType>(bool merge = false)
		{
			_converters.Add(new AutoMapConverter<FromType, ToType>(merge));
		}

		/// <summary>
		/// Registers two AutoMapConverters to support bi directional mapping between the two objects.
		/// </summary>
		/// <typeparam name="T1">Type that is two way mapped with <see cref="T2"/></typeparam>
		/// <typeparam name="T2">Type that is two way mapped with <see cref="T1"/></typeparam>
		/// <param name="merge">If <value>true</value> source object will merge with destination; otherwise will replace entire object.</param>
		public void RegisterTwoWayMapping<T1, T2>(bool merge = false)
		{
			_converters.Add(new AutoMapConverter<T1, T2>(merge));
			_converters.Add(new AutoMapConverter<T2, T1>(merge));
		}
	}
}
