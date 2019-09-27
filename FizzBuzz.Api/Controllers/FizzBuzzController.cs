using System.Collections.Generic;
using FizzBuzz.Api.Models.FizzBuzz;
using FizzBuzz.Business.FizzBuzz;
using FizzBuzz.Framework.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzz.Api.Controllers
{
	[ApiController]
	public class FizzBuzzController : ControllerBase
	{
		private readonly IFizzBuzzService _fizzBuzzService;
		private readonly ITypeMapper _mapper;

		public FizzBuzzController(IFizzBuzzService fizzBuzzService, ITypeMapper mapper)
		{
			_fizzBuzzService = fizzBuzzService;
			_mapper = mapper;
		}

		[HttpGet("v1/fizz-buzz/one-to-hundred")]
		public List<FizzBuzzResponse> GetFizzBuzzOneToHundred()
		{
			var messages = _fizzBuzzService.GetFizzBuzzMessages(1, 100);

			return _mapper.MapList<FizzBuzzResponse>(messages);
		}

		[HttpGet("v1/fizz-buzz/{number:int}")]
		public FizzBuzzResponse GetFizzBuzzByNumber([FromRoute]int number)
		{
			var message = _fizzBuzzService.GetFizzBuzzMessage(number);

			return _mapper.Map<FizzBuzzResponse>(message);
		}

		[HttpGet("v1/fizz-buzz/range")]
		public List<FizzBuzzResponse> GetFizzBuzzByNumberSet([FromQuery]FizzBuzzRequest request)
		{
			var message = _fizzBuzzService.GetFizzBuzzMessages(request.LowerLimit, request.UpperLimit);

			return _mapper.MapList<FizzBuzzResponse>(message);
		} 
	}
}
