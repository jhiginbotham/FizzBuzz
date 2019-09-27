using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FizzBuzz.Framework.Mapping
{
	public class BaseMappingModule : IMappingModule
	{
		public IEnumerable<IConverter> GetConverters()
		{
			var converterType = typeof(IConverter);
			var converters = GetType().GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Instance).Where(t => t.IsClass && converterType.IsAssignableFrom(t));
			return converters.Select(converter => (IConverter)Activator.CreateInstance(converter));
		}
	}
}
