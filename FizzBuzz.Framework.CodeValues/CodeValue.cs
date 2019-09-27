using System.Diagnostics;

namespace FizzBuzz.Framework.CodeValues
{
	[DebuggerDisplay("{Code} - {Value}")]
	public class CodeValue
	{
		public string Code { get; }
		public string Value { get; }

		public CodeValue(string code, string value)
		{
			Code = code;
			Value = value;
		}

		public static implicit operator string(CodeValue value)
		{
			return value == null ? null : value.ToString();
		}

		public override string ToString()
		{
			return Code;
		}

		public static bool operator ==(CodeValue code1, CodeValue code2)
		{
			return code1?.ToString() == code2?.ToString();
		}

		public static bool operator !=(CodeValue code1, CodeValue code2)
		{
			return !(code1 == code2);
		}

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case null:
					return false;
				case string _:
				case CodeValue _:
					return Code == obj.ToString();
				default:
					return false;
			}
		}

		public override int GetHashCode()
		{
			return (Code ?? string.Empty).GetHashCode();
		}
	}
}
