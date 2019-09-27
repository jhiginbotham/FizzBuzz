using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace FizzBuzz.Framework.Mapping
{
	public interface ITypeMapper
	{
		TDest Map<TSource, TDest>(TSource source);
		TDest Map<TSource, TDest>(TSource source, TDest destination);
		TDest Map<TDest>(object source);
		TDest Map<TDest>(object source, TDest destination);

		List<TDestination> MapList<TDestination>(IEnumerable source);
		List<TDestination> MapList<TDestination>(IEnumerable source, Type sourceType);

		ITypeMapper RegisterAssembly(Assembly mappingAssembly);
	}
}
