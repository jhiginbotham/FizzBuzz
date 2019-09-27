using FizzBuzz.Framework.CodeValues;

namespace FizzBuzz.Business.Models.FizzBuzz
{
	public class MessageType : CodeValue
	{
		public static readonly MessageType Fizz = new MessageType("FZ", "Fizz");
		public static readonly MessageType Buzz = new MessageType("BZ", "Buzz");
		public static readonly MessageType FizzBuzz = new MessageType("FBZ", "FizzBuzz");

		private static readonly CodeValueCollection<MessageType> _collection = new CodeValueCollection<MessageType>();

		public MessageType(string code, string value) : base(code, value)
		{
		}

		public static implicit operator MessageType(string str)
		{
			return _collection.GetCodeValue(str);
		}
	}
}
