using System;
using System.Runtime.InteropServices;
using System.Security;
using Newtonsoft.Json;

namespace FizzBuzz.Framework.Json.Converters
{
	internal class SecureStringConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(SecureString);
		}

		public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
		{
			var s = value as SecureString;
			if (s != null)
			{
				writer.WriteValue(ToStringValue(s));
			}
			else
			{
				writer.WriteNull();
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
		{
			if (reader.ValueType == typeof(string))
			{
				var value = (string)reader.Value;
				return ToSecureString(value);
			}

			return null;
		}

		private SecureString ToSecureString(string str)
		{
			var secureString = new SecureString();

			if (!string.IsNullOrEmpty(str))
			{
				foreach (char ch in str)
				{
					secureString.AppendChar(ch);
				}
			}

			return secureString;
		}

		private string ToStringValue(SecureString secureString)
		{
			if (secureString == null || secureString.Length == 0)
			{
				return null;
			}

			int length = secureString.Length;
			IntPtr pointer = IntPtr.Zero;
			char[] chars = new char[length];

			try
			{
				pointer = Marshal.SecureStringToBSTR(secureString);
				Marshal.Copy(pointer, chars, 0, length);

				return string.Join("", chars);
			}
			finally
			{
				if (pointer != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(pointer);
				}
			}
		}
	}
}
