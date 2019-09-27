using FizzBuzz.Api.Models.FizzBuzz;
using FizzBuzz.Business.Models.FizzBuzz;
using FizzBuzz.Framework.Mapping;
using FizzBuzz.Framework.Mapping.Testing;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzBuzz.Mapping.Api.Test
{
	[TestFixture]
	public class FizzBuzzResponseMappingTest
	{
		private TypeMapper _target;
		private ITypeMapper _typeMapper;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_target = new TypeMapper();
			_target.RegisterAssembly(typeof(FizzBuzzMappingHook).Assembly);
		}

		[SetUp]
		public void SetUp()
		{
			_typeMapper = MockRepository.GenerateMock<ITypeMapper>();
			_target.Mapper = _typeMapper;
		}

		#region Register Mappers

		[Test]
		public void RegisterAssembly_Should_Register_Expected_Mappings()
		{
			Assert.IsTrue(_target.HasMappingRegistered<NumericMessage, FizzBuzzResponse>());
		}

		#endregion

		#region ToFizzBuzzResponseMapping

		[Test]
		public void ToFizzBuzzResponseMapping_Should_Map_As_Expected()
		{
			var input = new NumericMessage
			{
				NumericValue = 1,
				Message = "1"
			};

			var expected = new FizzBuzzResponse
			{
				NumericValue = 1,
				Message = "1"
			};

			var actual = _target.Map<FizzBuzzResponse>(input);

			ObjectAssert.AreEqualRecursively(expected, actual);
		}

		#endregion
	}
}
