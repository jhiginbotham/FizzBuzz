using FizzBuzz.Business.Implementation.FizzBuzz;
using NUnit.Framework;

namespace FizzBuzz.Business.FizzBuzz.Test
{
	[TestFixture]
	public class FizzBuzzServiceTest
	{
		private FizzBuzzService _target;

		[SetUp]
		public void SetUp()
		{
			_target = new FizzBuzzService();
		}

		[Test]
		[TestCase(3)]
		[TestCase(6)]
		[TestCase(9)]
		public void GetFizzBuzzMessage_When_Number_Is_Divisible_By_Three_And_Not_Five_Should_Return_Fizz(int number)
		{
			var result = _target.GetFizzBuzzMessage(number);

			Assert.AreEqual("Fizz", result.Message);
		}

		[Test]
		[TestCase(5)]
		[TestCase(10)]
		[TestCase(20)]
		public void GetFizzBuzzMessage_When_Number_Is_Divisible_By_Five_And_Not_Three_Should_Return_Buzz(int number)
		{
			var result = _target.GetFizzBuzzMessage(number);

			Assert.AreEqual("Buzz", result.Message);
		}

		[Test]
		[TestCase(15)]
		[TestCase(30)]
		[TestCase(60)]
		public void GetFizzBuzzMessage_When_Number_Is_Divisible_By_Three_And_Five_Should_Return_FizzBuzz(int number)
		{
			var result = _target.GetFizzBuzzMessage(number);

			Assert.AreEqual("FizzBuzz", result.Message);
		}

		[Test]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(4)]
		public void GetFizzBuzzMessage_When_Number_Is_Not_Divisible_By_Three_Or_Five_Should_Return_Number(int number)
		{
			var result = _target.GetFizzBuzzMessage(number);

			Assert.AreEqual(number.ToString(), result.Message);
		}

		[Test]
		public void GetFizBzuzzMessages_Should_Execute_Results_From_GetFizzBuzzMessage()
		{
			var result = _target.GetFizzBuzzMessages(1, 15);

			Assert.AreEqual("1", result[0].Message);
			Assert.AreEqual("2", result[1].Message);
			Assert.AreEqual("Fizz", result[2].Message);
			Assert.AreEqual("4", result[3].Message);
			Assert.AreEqual("Buzz", result[4].Message);
			Assert.AreEqual("Fizz", result[5].Message);
			Assert.AreEqual("7", result[6].Message);
			Assert.AreEqual("8", result[7].Message);
			Assert.AreEqual("Fizz", result[8].Message);
			Assert.AreEqual("Buzz", result[9].Message);
			Assert.AreEqual("11", result[10].Message);
			Assert.AreEqual("Fizz", result[11].Message);
			Assert.AreEqual("13", result[12].Message);
			Assert.AreEqual("14", result[13].Message);
			Assert.AreEqual("FizzBuzz", result[14].Message);
		}
	}
}
