using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
	class DefaultNumberValidator : INumberValidator
	{
		public void Validate(string data, IEnumerable<int> numbers)
		{
			var negatives = GetNegativeValues(numbers).ToArray();
			if (negatives.Any())
				throw new UnparseableDataException(data).ContainsNegatives(negatives);
		}

		private static IEnumerable<int> GetNegativeValues(IEnumerable<int> numbers)
		{
			return numbers.Where(i => i < 0);
		}
	}
}