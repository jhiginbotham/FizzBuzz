using System;
using System.Collections.Generic;
using System.Text;

namespace FizzBuzz.Framework.Mapping
{
	public class AutoMapConverter<TFromType, TToType> : BaseConverter<TFromType, TToType>
	{
		private bool _merge;
		public AutoMapConverter(bool merge = false)
		{
			_merge = merge;
		}

		public sealed override TToType ConvertType(TFromType source, TToType destination, ITypeMapper mapper)
		{
			var content = AutoMap<TToType>(source);

			if (_merge)
			{
				Merge(destination, content);
			}
			else
			{
				destination = content;
			}

			return destination;
		}
	}
}
