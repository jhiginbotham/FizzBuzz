using System.Collections.Generic;
using FizzBuzz.Api.Models.FizzBuzz;
using FizzBuzz.Business.FizzBuzz;
using FizzBuzz.Business.Models.FizzBuzz;
using FizzBuzz.Framework.Mapping;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzBuzz.Api.Controllers.Test
{
	[TestFixture]
	public class FizzBuzzControllerTest
	{
		private FizzBuzzController _target;
		private IFizzBuzzService _fizzBuzzService;
		private ITypeMapper _mapper;

		[SetUp]
		public void SetUp()
		{
			_fizzBuzzService = MockRepository.GenerateMock<IFizzBuzzService>();
			_mapper = MockRepository.GenerateMock<ITypeMapper>();

			_target = new FizzBuzzController(_fizzBuzzService, _mapper);
		}

		#region GetFizzBuzzOneToHundred

		[Test]
		public void GetFizzBuzzOneToHundred_Should_Call_FizzBuzzService_With_Predefined_Request()
		{
			_fizzBuzzService.Expect(x => x.GetFizzBuzzMessages(1, 100));

			_target.GetFizzBuzzOneToHundred();

			_fizzBuzzService.VerifyAllExpectations();
		}

		[Test]
		public void GetFizzBuzzOneToHundred_Should_Return_Mapped_Response()
		{
			var expected = new List<FizzBuzzResponse>();
			var numericMessage = new List<NumericMessage>();

			_fizzBuzzService.Stub(x => x.GetFizzBuzzMessages(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Return(numericMessage);
			_mapper.Expect(x => x.MapList<FizzBuzzResponse>(numericMessage)).Return(expected);

			var result = _target.GetFizzBuzzOneToHundred();

			_mapper.VerifyAllExpectations();
			Assert.AreEqual(expected, result);
		}

		#endregion

		#region GetFizzBuzzByNumber

		[Test]
		public void GetFizzBuzzByNumber_Should_Call_FizzBuzzService_With_Predefined_Request()
		{
			_fizzBuzzService.Expect(x => x.GetFizzBuzzMessage(1));

			_target.GetFizzBuzzByNumber(1);

			_fizzBuzzService.VerifyAllExpectations();
		}

		[Test]
		public void GetFizzBuzzByNumber_Should_Return_Mapped_Response()
		{
			var expected = new FizzBuzzResponse();
			var numericMessage = new NumericMessage();

			_fizzBuzzService.Stub(x => x.GetFizzBuzzMessage(Arg<int>.Is.Anything)).Return(numericMessage);
			_mapper.Expect(x => x.Map<FizzBuzzResponse>(numericMessage)).Return(expected);

			var result = _target.GetFizzBuzzByNumber(1);

			_mapper.VerifyAllExpectations();
			Assert.AreEqual(expected, result);
		}

		#endregion

		#region GetFizzBuzzByNumberSet

		[Test]
		public void GetFizzBuzzByNumberSet_Should_Call_FizzBuzzService_With_Predefined_Request()
		{
			var request = new FizzBuzzRequest{LowerLimit = 1, UpperLimit = 2};
			_fizzBuzzService.Expect(x => x.GetFizzBuzzMessages(request.LowerLimit, request.UpperLimit));

			_target.GetFizzBuzzByNumberSet(request);

			_fizzBuzzService.VerifyAllExpectations();
		}

		[Test]
		public void GetFizzBuzzByNumberSet_Should_Return_Mapped_Response()
		{
			var request = new FizzBuzzRequest { LowerLimit = 1, UpperLimit = 2 };
			var expected = new List<FizzBuzzResponse>();
			var numericMessage = new List<NumericMessage>();

			_fizzBuzzService.Stub(x => x.GetFizzBuzzMessages(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Return(numericMessage);
			_mapper.Expect(x => x.MapList<FizzBuzzResponse>(numericMessage)).Return(expected);

			var result = _target.GetFizzBuzzByNumberSet(request);

			_mapper.VerifyAllExpectations();
			Assert.AreEqual(expected, result);
		}

		#endregion
	}
}
