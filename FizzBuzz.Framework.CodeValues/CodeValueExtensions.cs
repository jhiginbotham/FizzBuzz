using System.Collections.Generic;
using System.Linq;

namespace FizzBuzz.Framework.CodeValues
{
	public static class CodeValueExtensions
	{
		public static bool Contains<T>(this IEnumerable<T> collection, string value) where T : CodeValue
		{
			return collection.Any(x => x.Code == value);
		}
	}
}
