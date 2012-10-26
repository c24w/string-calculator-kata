using System.Collections.Generic;

namespace StringCalculator.Validation
{
	public interface INumberValidator
	{
		void Validate(string rawData, IEnumerable<int> numbers);
	}
}