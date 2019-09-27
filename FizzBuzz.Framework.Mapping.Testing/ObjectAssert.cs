using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace FizzBuzz.Framework.Mapping.Testing
{
	public static class ObjectAssert
	{
		/// <summary>
		/// Asserts that each public property are equal on each object.
		/// </summary>
		/// <typeparam name="T">Type of the object being compared.</typeparam>
		/// <param name="expected">Expected object.</param>
		/// <param name="actual">Actual object.</param>
		/// <param name="ignoreProps">Property names that should be omitted from comparison.</param>
		public static void AreEqual<T>(T expected, T actual, IEnumerable<string> ignoreProps = null) where T : class
		{
			if (ignoreProps == null)
			{
				ignoreProps = new List<string>();
			}

			if (expected != null && actual != null)
			{
				Type type = typeof(T);
				foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
				{
					if (!ignoreProps.Contains(pi.Name))
					{
						var expectedValue = type.GetProperty(pi.Name).GetValue(expected, null);
						var actualValue = type.GetProperty(pi.Name).GetValue(actual, null);

						Assert.AreEqual(expectedValue, actualValue, "Auto compare failed on property: " + pi.Name);
					}
				}
			}
			else
			{
				Assert.AreEqual(expected, actual);
			}
		}

		public static void AreEqualRecursively<T>(T expected, T actual, params string[] ignoreProps)
		{
			RecursiveObjectComparison(expected, actual, ignoreProps, typeof(T).FullName, true);
		}

		public static bool IsEqualRecursively<T>(T expected, T actual, params string[] ignoreProps)
		{
			return RecursiveObjectComparison(expected, actual, ignoreProps, typeof(T).FullName, false);
		}

		private static bool RecursiveObjectComparison(object expected, object actual, IEnumerable<string> ignoreList, string objectName, bool assertFailures)
		{
			var objectsEqual = true;
			if (expected != null && actual != null)
			{
				var objectType = expected.GetType();

				if (CanDirectlyCompare(objectType))
				{
					objectsEqual &= AreValuesEqual(expected, actual, objectName, "root", assertFailures);
				}
				else if (!CanDirectlyCompare(objectType) && typeof(IEnumerable).IsAssignableFrom(objectType))
				{
					objectsEqual &= CompareCollection((IEnumerable)expected, (IEnumerable)actual, objectName, string.Empty, assertFailures);
				}
				else
				{
					foreach (PropertyInfo propertyInfo in objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && !ignoreList.Contains(p.Name)))
					{
						object expectedValue;
						object actualValue;

						expectedValue = propertyInfo.GetValue(expected, null);
						actualValue = propertyInfo.GetValue(actual, null);

						// if it is a primative type, value type or implements IComparable, just directly try and compare the value
						if (CanDirectlyCompare(propertyInfo.PropertyType))
						{
							objectsEqual &= AreValuesEqual(expectedValue, actualValue, objectName, propertyInfo.Name, assertFailures);
						}
						// if it implements IEnumerable, then scan any items
						else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
						{
							objectsEqual &= CompareCollection((IEnumerable)expectedValue, (IEnumerable)actualValue, objectName, propertyInfo.Name, assertFailures);
						}
						else if (propertyInfo.PropertyType.IsClass)
						{
							objectsEqual &= RecursiveObjectComparison(propertyInfo.GetValue(expected, null), propertyInfo.GetValue(actual, null), ignoreList, GetPropertyName(objectName, propertyInfo.Name), assertFailures);
						}
						else
						{
							throw new Exception(String.Format("Cannot compare property '{0}.{1}'.", objectType.FullName, propertyInfo.Name));
						}
					}
				}
			}
			else
			{
				objectsEqual &= AreValuesEqual(expected, actual, objectName, string.Empty, assertFailures);
			}

			return objectsEqual;
		}

		private static bool CompareCollection(IEnumerable expectedCollection, IEnumerable actualCollection, string objectName, string propertyName, bool assertFailures)
		{
			var collectionsEqual = true;
			if (expectedCollection == null && actualCollection == null)
			{
				return collectionsEqual;
			}

			if (expectedCollection == null && actualCollection != null || expectedCollection != null && actualCollection == null)
			{
				collectionsEqual &= AreValuesEqual(expectedCollection, actualCollection, objectName, propertyName, assertFailures);
			}

			var expected = expectedCollection.Cast<object>();
			var actual = actualCollection.Cast<object>();
			var expectedCount = expected.Count();
			var actualCount = actual.Count();

			collectionsEqual &= AreValuesEqual(expectedCount, actualCount, objectName, propertyName + ".Count", assertFailures);

			// compare each item... this assumes both collections have the same order
			for (int i = 0; i < expectedCount; i++)
			{
				object collectionItem1;
				object collectionItem2;

				collectionItem1 = expected.ElementAt(i);
				collectionItem2 = actual.ElementAt(i);

				if (collectionItem1 == null && collectionItem2 == null)
				{
					collectionsEqual &= AreValuesEqual(collectionItem1, collectionItem2, objectName, propertyName, assertFailures);
				}
				else if (CanDirectlyCompare((collectionItem1 ?? collectionItem2).GetType())) // Compare Values
				{
					collectionsEqual &= AreValuesEqual(collectionItem1, collectionItem2, objectName, propertyName + "[" + i + "]", assertFailures);
				}
				else    //Compare objects
				{
					collectionsEqual &= RecursiveObjectComparison(collectionItem1, collectionItem2, new List<string>(), GetPropertyName(objectName, propertyName + "[" + i + "]"), assertFailures);
				}
			}

			return collectionsEqual;
		}

		/// <summary>
		/// Determines whether value instances of the specified type can be directly compared.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		///   <c>true</c> if this value instances of the specified type can be directly compared; otherwise, <c>false</c>.
		/// </returns>
		private static bool CanDirectlyCompare(Type type)
		{
			return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
		}

		private static bool AreValuesEqual(object valueA, object valueB, string objectName, string propertyName, bool assertFailures)
		{
			var propName = GetPropertyName(objectName, propertyName);
			if (assertFailures)
			{
				Assert.AreEqual(valueA, valueB, "Failed comparison on property: " + propName);
				return true;
			}

			return Is.EqualTo(valueB).ApplyTo(valueA).IsSuccess;
		}

		private static string GetPropertyName(string objectName, string propertyName)
		{
			if (!String.IsNullOrEmpty(objectName) && !String.IsNullOrEmpty(propertyName))
			{
				return objectName + "." + propertyName;
			}

			return objectName + propertyName;
		}

		public static T GetPropertyValue<T>(this object myObject, string propertyName)
		{
			var property = myObject.GetType().GetProperty(propertyName);

			return (T)property.GetValue(myObject, null);
		}
	}

}
