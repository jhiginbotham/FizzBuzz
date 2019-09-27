using System.Collections.Generic;
using FizzBuzz.Business.Models.FizzBuzz;

namespace FizzBuzz.Business.FizzBuzz
{
	public interface IFizzBuzzService
	{
		NumericMessage GetFizzBuzzMessage(int value);

		List<NumericMessage> GetFizzBuzzMessages(int lowerLimit, int upperLimit);
	}
}
