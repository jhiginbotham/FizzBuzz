using System;
using System.Collections.Generic;
using System.Text;

namespace FizzBuzz.Framework.Mapping
{
	public interface IMappingModule
	{
		IEnumerable<IConverter> GetConverters();
	}
}
