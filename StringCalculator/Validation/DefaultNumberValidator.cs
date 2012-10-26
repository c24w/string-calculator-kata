using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.Validation
{
	class DefaultNumberValidator : INumberValidator
	{
		public void Validate(string rawData, IEnumerable<int> numbers)
		{
			var negatives = GetNegativeValues(numbers).ToArray();
			if (negatives.Any())
				throw new UnparseableDataException(rawData).ContainsNegatives(negatives);
		}

		private static IEnumerable<int> GetNegativeValues(IEnumerable<int> numbers)
		{
			return numbers.Where(i => i < 0);
		}
	}
}