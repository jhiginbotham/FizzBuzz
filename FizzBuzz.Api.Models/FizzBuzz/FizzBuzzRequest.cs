using System.ComponentModel.DataAnnotations;

namespace FizzBuzz.Api.Models.FizzBuzz
{
	/// <summary>
	/// The Upper and Lower limit bounds that FizzBuzz will execute against.
	/// </summary>
	public class FizzBuzzRequest
	{
		/// <summary>
		/// The number to start with.
		/// </summary>
		[Required]
		public int LowerLimit { get; set; }
		/// <summary>
		/// The number to end with.
		/// </summary>
		[Required]
		public int UpperLimit { get; set; }
	}
}
