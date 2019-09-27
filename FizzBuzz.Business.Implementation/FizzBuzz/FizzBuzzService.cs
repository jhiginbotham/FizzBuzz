using System.Collections.Generic;
using FizzBuzz.Business.FizzBuzz;
using FizzBuzz.Business.Models.FizzBuzz;

namespace FizzBuzz.Business.Implementation.FizzBuzz
{
	public class FizzBuzzService : IFizzBuzzService
	{
		public NumericMessage GetFizzBuzzMessage(int value)
		{
			var numericMessage = new NumericMessage
			{
				NumericValue = value
			};

			if (IsDivisibleByThree(value) && IsDivisibleByFive(value))
			{
				numericMessage.Message = MessageType.FizzBuzz.Value;
				return numericMessage;
			}

			if (IsDivisibleByThree(value))
			{
				numericMessage.Message = MessageType.Fizz.Value;
				return numericMessage;
			}

			if (IsDivisibleByFive(value))
			{
				numericMessage.Message = MessageType.Buzz.Value;
				return numericMessage;
			}

			numericMessage.Message = value.ToString();
			return numericMessage;
		}

		public List<NumericMessage> GetFizzBuzzMessages(int lowerLimit, int upperLimit)
		{
			var messages = new List<NumericMessage>();

			for (int i = lowerLimit; i <= upperLimit; i++)
			{
				messages.Add(GetFizzBuzzMessage(i));
			}

			return messages;
		}

		private bool IsDivisibleByFive(int value)
		{
			return value % 5 == 0;
		}

		private bool IsDivisibleByThree(int value)
		{
			return value % 3 == 0;
		}
	}
}
