using System.Collections.Generic;

namespace StringCalculator.Validation
{
	public interface INumberValidator
	{
		void Validate(IEnumerable<int> numbers);
	}
}