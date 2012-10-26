using System.Collections.Generic;

namespace StringCalculator
{
	public interface INumberValidator
	{
		void Validate(string data, IEnumerable<int> numbers);
	}
}