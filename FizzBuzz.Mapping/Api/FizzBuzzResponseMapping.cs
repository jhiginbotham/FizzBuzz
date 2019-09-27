using FizzBuzz.Api.Models.FizzBuzz;
using FizzBuzz.Business.Models.FizzBuzz;
using FizzBuzz.Framework.Mapping;

namespace FizzBuzz.Mapping.Api
{
	public class FizzBuzzResponseMapping : BaseMappingModule
	{
		private class ToFizzBuzzResponseMapping : BaseConverter<NumericMessage, FizzBuzzResponse>
		{
			public override FizzBuzzResponse ConvertType(NumericMessage source, FizzBuzzResponse destination, ITypeMapper mapper)
			{
				destination.NumericValue = source.NumericValue;
				destination.Message = source.Message;

				return destination;
			}
		}
	}
}
