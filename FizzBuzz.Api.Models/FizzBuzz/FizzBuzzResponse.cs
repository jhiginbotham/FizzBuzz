namespace FizzBuzz.Api.Models.FizzBuzz
{
	/// <summary>
	/// The result of the FizzBuzz calculation.
	/// </summary>
	public class FizzBuzzResponse
	{
		/// <summary>
		/// The number being calculated.
		/// </summary>
		public int NumericValue { get; set; }
		/// <summary>
		/// The result of the calculation.
		/// </summary>
		public string Message { get; set; }
	}
}
